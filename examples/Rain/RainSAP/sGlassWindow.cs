using PdPlusPlusSAP;

public struct sGlassWindow
{

    private PdPlusPlusSAP.Delay del1;
    private PdPlusPlusSAP.Delay del2;
    private PdPlusPlusSAP.BandPass bp1;
    private PdPlusPlusSAP.BandPass bp2;
    private PdPlusPlusSAP.BandPass bp3;
    private PdPlusPlusSAP.BandPass bp4;
    private PdPlusPlusSAP.Oscillator osc1;
    private PdPlusPlusSAP.Oscillator osc2;
    private PdPlusPlusSAP.Oscillator osc3;
    private PdPlusPlusSAP.Oscillator osc4;
    private double bpCf1;
    private double bpCf2;
    private double bpCf3;
    private double bpCf4;
    private double bpQ;
    private double f1;
    private double f2;
    private double f3;
    private double f4;
    private double vol;
    private double delTime1;
    private double delTime2;
    private double delread1;
    private double delread2;

    public void Create()
    {
        // Initialize any state here.
        del1.Create();
        del2.Create();
        bp1.Create();
        bp2.Create();
        bp3.Create();
        bp4.Create();
        osc1.Create();
        osc2.Create();
        osc3.Create();
        osc4.Create();

        bpCf1 = 2007;
        bpCf2 = 1994;
        bpCf3 = 1984;
        bpCf4 = 1969;
        bpQ = 2.3;
        f1 = 254;
        f2 = 669;
        f3 = 443;
        f4 = 551;
        vol = .61;
        delTime1 = 3.7;
        delTime2 = 4.2;
        delread1 = 0;
        delread2 = 0;

        bp1.setQ(bpQ);
        bp2.setQ(bpQ);
        bp3.setQ(bpQ);
        bp4.setQ(bpQ);
        bp1.setCenterFrequency(bpCf1);
        bp2.setCenterFrequency(bpCf2);
        bp3.setCenterFrequency(bpCf3);
        bp4.setCenterFrequency(bpCf4);
        del1.setDelayTime(delTime1);
        del2.setDelayTime(delTime2);
    }

    public double perform(double input)
    {
        double output;
        double oscVol1 = bp1.perform(delread1 * vol);
        double oscVol2 = bp2.perform(delread1 * vol);
        double oscVol3 = bp3.perform(delread2 * vol);
        double oscVol4 = bp4.perform(delread2 * vol);
        double a = (osc2.perform(f2) * oscVol2) + (osc4.perform(f4) * oscVol4);
        double b = (osc1.perform(f1) * oscVol1) + (osc3.perform(f3) * oscVol3);
        delread1 = del1.perform(input + a);
        delread2 = del2.perform(input + b);
        output = (delread1 * vol) + (delread2 * vol);
        return output;
    }

    public void Dispose()
    {
        // Clean up any state here.
        del1.Dispose();
        del2.Dispose();
        bp1.Dispose();
        bp2.Dispose();
        bp3.Dispose();
        bp4.Dispose();
        osc1.Dispose();
        osc2.Dispose();
        osc3.Dispose();
        osc4.Dispose();
    }

    
}
