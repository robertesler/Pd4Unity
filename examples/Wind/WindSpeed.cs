using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

public class WindSpeed
{
    
    private Oscillator osc = new Oscillator();
    private Noise noise = new Noise();
    private BandPass bp1 = new BandPass();
    private Noise noise2 = new Noise();
    private BandPass bp2 = new BandPass();

    public void Dispose()
    {
        osc.Dispose();
        noise.Dispose();
        bp1.Dispose();
        noise2.Dispose();
        bp2.Dispose();
        Debug.Log("WindSpeed: Memory Deleted");
    }

    ~WindSpeed()
    {
       
    }


    public double perform(double f)
    {
        double windOut = 0;
        double w = (osc.perform(f) + 1) * .25;
        double gust = this.gust(w);
        double squall = this.squall(w);
        double mix = w + gust + squall;
        windOut = this.clip(mix, 0, 1);
        return windOut;
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

    //emulate [max~] a = input, b = input2, always return the higher value
    private double max(double a, double b)
    {
        double max = 0;
        if (a < b)
        {
            max = b;
        }
        if (a > b)
        {
            max = a;
        }
        return max;
    }

    private double gust(double input)
    {
        double windOut = 0;
        bp1.setCenterFrequency(.25);
        double n = noise.perform();
        double b = bp1.perform(n) * 25;
        //println(b);
        double i = ((input + .5) * (input + .5)) - .125;
        windOut = b * i;
        return windOut;
    }

    private double squall(double input)
    {
        double windOut = 0;
        bp2.setCenterFrequency(1.5);
        double i = (max(input, .4) - .4) * 8;
        i *= i;//squared
        double n = noise.perform();
        double b = bp2.perform(n) * 10;

        windOut = b * i;
        return windOut;
    }

   
}
