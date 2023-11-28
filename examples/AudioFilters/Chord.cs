using UnityEngine;
using PdPlusPlus;

/*
A simple synth
*/
public class Chord
{
    private Phasor phasor = new Phasor();
    private Cosine cos = new Cosine();

    public double perform(double freq)
    {
        double output = 0;
        double sig = (phasor.perform(freq) - .5) * 3;
        output = cos.perform(clip(sig, -0.5, 0.5));
        return output;
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

    public void Dispose()
    {
        phasor.Dispose();
        cos.Dispose();

    }


}
