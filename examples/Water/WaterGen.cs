using System;
using PdPlusPlus;

public class WaterGen  
{
    private Line line = new Line();
    private Oscillator osc = new Oscillator();
    private LowPass lop = new LowPass();
    private Random rand = new Random();
    private double previousSample = 0;
    private long sampleRate = 44100;
    private double bexp = 0;
    private long counter = 0;
    private double lastRate = 0;

    public WaterGen()
    {
        osc.setSampleRate(sampleRate);
    }

    public WaterGen(long sr)
    {
        osc.setSampleRate(sr);
    }

    public void Dispose()
    {
        line.Dispose();
        osc.Dispose();
        lop.Dispose();
    }

    /*
     This takes our bilinear random number to generate
     frequencies to our oscillator * an envelope generator. 
    */
    public double perform(double rate, double freq, double depth, double slew)
    {
        double output = 0;
        bool bang = metro(rate, sampleRate);
        if (bang)
        {
            bexp = bilexp();
            bexp *= freq;
            bexp += depth;
        }
        double f = line.perform(bexp, slew);
        double fexpr = f - previousSample;//emulates our FIR filter
        double c = clip(fexpr, 0, 1);
        lop.setCutoff(10);
        double v = lop.perform(c) * .9;
        output = osc.perform(f) * v * v;
        previousSample = f;
        return output * 2;
    }

    //Emulates the metro object, based on samplerate, not real-time.
    private bool metro(double ms, long sampleRate)
    {
        bool bang = false;
        long tick = sampleRate / 1000 * (long)ms;

        if (lastRate != ms) counter = 0;

        if (counter++ == tick)
        {
            counter = 0;
            bang = true;
        }
        lastRate = ms;
        return bang;
    }

    //emulate [clip~], a = input, b = low range, c = high range
    private double clip(double a, double b, double c)
    {
        if (a < b)
            return b;
        else if (a > c)
            return c;
        else
            return a;
    }

    //This is a bilinear exponential random number generator, a la Andy Farnell
    private double bilexp()
    {
        double be = 0;
        double r = rand.Next(0, 8192);
        double m = r % 4096;
        double x = Math.Exp((float)(m / 4096) * 9);
        double v = 0;

        if (r > 4096)
            v = 1;
        else
            v = -1;

        be = (x * v) / 23000;
        return be;
    }
}
