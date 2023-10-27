using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

public class MyMusic
{
    
    private double frequency = 400.0F;
    private double frequency2 = 2.0F;
    private double gain = 0.2F;
    public double outputL = 0;
    public double outputR = 0;
    private Noise noise = new Noise();
    private BandPass bp = new BandPass();
    private Oscillator osc = new Oscillator();
    private Oscillator osc2 = new Oscillator();
    

    // Start is called before the first frame update, put your instatiation code here
    void Start()
    {
        bp.setQ(2);
    }

    public void runAlgorithm(double inputL, double inputR)
    {
        bp.setCenterFrequency(frequency2);
        outputL = outputR = (float)osc.perform(frequency) * (float)osc2.perform(frequency2) * gain +
            (bp.perform(noise.perform()) * .05);
    }

    /*
     Make sure to release all of your Pd++ objects here
     */
    public void Dispose()
    {
        osc.Dispose();
        osc2.Dispose();
        noise.Dispose();
        bp.Dispose();
    }

    #region setters
    public void setFreq1(double f)
    {
        frequency = f;
    }

    public void setFreq2(double f)
    {
        frequency2 = f;
    }

    public void setGain(double g)
    {
        gain = g;
    }
    #endregion setters
}
