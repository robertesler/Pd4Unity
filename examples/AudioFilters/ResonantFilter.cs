using System;
using UnityEngine;
using PdPlusPlus;

public class ResonantFilter
{ 
    private RealZero rzero1 = new RealZero();
    private RealZero rzero2 = new RealZero();
    private ComplexPole cpole1 = new ComplexPole();
    private ComplexPole cpole2 = new ComplexPole();
    private Noise noise = new Noise();
    private double freq = 100;
    private double q = 1;
    private int scale = 2;

// Start is called before the first frame update
    ResonantFilter()
    {
        int sr = AudioSettings.outputSampleRate;
        noise.setSampleRate(sr);
    }

    ~ResonantFilter()
    {

    }
   
    public void Dispose()
    {
        rzero1.Dispose();
        rzero2.Dispose();
        cpole1.Dispose();
        cpole2.Dispose();
        noise.Dispose();
    }

    //4 stage 2-pole, 2-zero resonant filter
    public double perform(double in1, double in2)
    {
        double output = 0;
        double n = noise.perform();
        double stage1 = rzero1.perform(n, -1);
        double stage2 = rzero2.perform(stage1, 1);
        double[] f = getFreq();
        double[] stage3 = cpole1.perform(stage2, 0, f[0], f[1]);
        double[] stage4 = cpole2.perform(stage3[0], stage3[1], f[0], f[1] * -1);
        output = stage4[0] * getScale();
        return output * (1 / (float)scale);

    }


    //We use synchronized to communicate with the audio thread
    public void setFreq(double f1)
    {
        freq = f1;
    }

    //returns real and imaginary parts, 2*PI*T
    private double[] getFreq()
    {
        double[] output = { 0, 0 };
        float twoPiT = (float)freq * (float)((Math.Atan(1) * 8) / noise.getSampleRate());
        double sin = Math.Sin(twoPiT);
        double cos = Math.Cos(twoPiT);
        output[0] = cos * getQ();
        output[1] = sin * getQ();
        return output;
    }

    public void setQ(double _q)
    {
        q = _q;
    }

    //PI * B * T
    private double getQ()
    {
        double output = 0;
        if (q <= 0) q = .001;
        double i = freq / q;
        float piBT = (float)i * (float)(Math.Atan(1) * -4) / noise.getSampleRate();
        output = Math.Exp(piBT);

        return output;
    }

    public void setScale(int s)
    {
        scale = s;
    }

    /*
    When creating raw filters you need a gain scale.
    The following cases are possible gain components.
    Case 0 is no gain scale, WHICH IS VERY LOUD!!!
    So you would need your own custom gain algorithm.
    Case 1 and 2 are (1-r^2)/2 or sqrt((1-r^2)/2) respectively.
    Be careful with these numbers.
    */
    double getScale()
    {
        switch (scale)
        {
            case 0:
                {
                    return 1;
                }
            case 1:
                {
                    double i = (1 - (getQ() * getQ())) * .5;
                    return i;
                }
            case 2:
                {
                    double i = (1 - (getQ() * getQ())) * .5;
                    return Math.Sqrt((float)i);
                }
            default:
                {
                    double i = (1 - (getQ() * getQ())) * .5;
                    return i;
                }
        }

    }
}
