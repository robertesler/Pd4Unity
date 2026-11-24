using PdPlusPlusSAP;

public struct sRain
{
    public double RainIntensity;
    public double RainVolume;
    public double bpCf;
    public double bpQ;
    public double bpGain;
    private Noise noise;
    private Noise noise2;
    private BandPass bp;
    private HighPass hip;
    private LowPass lop;
    private Delay del;
    private Drop drop;
    private sGlassWindow glassWindow;
    private double delread;
    private double bpRainVol;


    public void Create()
    {
        RainIntensity = 0.06F;
        RainVolume = 3.0F;
        bpCf = 1400;
        bpQ = 2.0F;
        bpGain = 1.0F;
        bpRainVol = 2.8F;
        delread = 0;
        noise.Create();
        noise2.Create();
        bp.Create();
        hip.Create();
        lop.Create();
        del.Create();
        glassWindow.Create();
        drop.Create();
        del.setDelayTime(300);
        hip.setCutoff(9000);
        lop.setCutoff(500);
        bp.setCenterFrequency(bpCf);
        bp.setQ(bpQ);
        
    }

    public void Dispose()
    {
        noise.Dispose();
        noise2.Dispose();
        hip.Dispose();
        bp.Dispose();
        lop.Dispose();
        del.Dispose();
        drop.Dispose();
        glassWindow.Dispose();
    }

    public void setIntensity(double intensity)
    {
        
    }

    public double perform()
    {
        double dropInput = (del.perform(delread) * 24) + 6;
        double n = noise.perform();
        double farFieldRain = bp.perform(n) * (bpRainVol * .01);
        double x = drop.perform(n, dropInput, RainIntensity, RainVolume);
        double win = hip.perform(x) * 20;
        delread = glassWindow.perform(win);
        return lop.perform(delread) + (farFieldRain * bpGain);
    }

}
