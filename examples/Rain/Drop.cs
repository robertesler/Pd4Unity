using UnityEngine;
using PdPlusPlus;

public class Drop
{

    private VoltageControlFilter vcf = new VoltageControlFilter();
    private double vcfOut;

    // Start is called before the first frame update
    public Drop()
    {
        vcf.setQ(.01);
    }

    public double perform(double sig, double cf, double r, double rv)
    {
        double output = 0;
        //vcf.setQ(.01);
        vcfOut = vcf.perform_real(sig, cf);
        double x = max(clip(vcfOut, 0, 1), r);
        double y = x - r;
        double z = y * y;
        output = (z * z) * rv;
        return output;
    }

    //emulate [clip~], a = input, b = low range, c = high range
    protected double clip(double a, double b, double c)
    {
        if (a < b)
            return b;
        else if (a > c)
            return c;
        else
            return a;
    }

    //emulate [max~] a = input, b = input2, always return the higher value
    protected double max(double a, double b)
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

    public void Dispose()
    {
       vcf.Dispose();
    }

}
