using UnityEngine;
using PdPlusPlus;

public class GlassWindow
{
    private Delay del1 = new Delay();
    private Delay del2 = new Delay();
    private BandPass bp1 = new BandPass();
    private BandPass bp2 = new BandPass();
    private BandPass bp3 = new BandPass();
    private BandPass bp4 = new BandPass();
    private Oscillator osc1 = new Oscillator();
    private Oscillator osc2 = new Oscillator();
    private Oscillator osc3 = new Oscillator();
    private Oscillator osc4 = new Oscillator();
    private double bpCf1 = 2007;
    private double bpCf2 = 1994;
    private double bpCf3 = 1984;
    private double bpCf4 = 1969;
    private double bpQ = 2.3;
    private double f1 = 254;
    private double f2 = 669;
    private double f3 = 443;
    private double f4 = 551;
    private double vol = .61;
    private double delTime1 = 3.7;
    private double delTime2 = 4.2;
    private double delread1 = 0;
    private double delread2 = 0;

    // Start is called before the first frame update
    public GlassWindow()
    {
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
        double output = 0;
        double oscVol1 = bp1.perform((delread1 * vol));
        double oscVol2 = bp2.perform((delread1 * vol));
        double oscVol3 = bp3.perform((delread2 * vol));
        double oscVol4 = bp4.perform((delread2 * vol));
        double a = (osc2.perform(f2) * oscVol2) + (osc4.perform(f4) * oscVol4);
        double b = (osc1.perform(f1) * oscVol1) + (osc3.perform(f3) * oscVol3);
        delread1 = del1.perform(input + a);
        delread2 = del2.perform(input + b);
        output = (delread1 * vol) + (delread2 * vol);
        return output;
    }

    public void Dispose()
    {
       
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
