using System;
using PdPlusPlus;

class Textures
{
    private Envelope env1 = new Envelope();
    private Envelope env2 = new Envelope();
    private Envelope env3 = new Envelope();
    private Envelope env4 = new Envelope();
    private Envelope env5 = new Envelope();

    //snow
    private Noise noise1 = new Noise();
    private Noise noise2 = new Noise();
    private LowPass lop1 = new LowPass();
    private LowPass lop2 = new LowPass();
    private LowPass lop3 = new LowPass();
    private LowPass lop4 = new LowPass();
    private LowPass lop5 = new LowPass();
    private HighPass hip = new HighPass();
    private VoltageControlFilter vcf = new VoltageControlFilter();

    //grass
    private Noise grass_noise = new Noise();
    private LowPass grass_lop1 = new LowPass();
    private LowPass grass_lop2 = new LowPass();
    private LowPass grass_lop3 = new LowPass();
    private HighPass grass_hip1 = new HighPass();
    private HighPass grass_hip2 = new HighPass();
    private Oscillator grass_osc = new Oscillator();
    private VoltageControlFilter grass_vcf = new VoltageControlFilter();

    //dirt
    private Noise dirt_noise = new Noise();
    private LowPass dirt_lop = new LowPass();
    private Oscillator dirt_osc1 = new Oscillator();
    private Oscillator dirt_osc2 = new Oscillator();
    private HighPass dirt_hip = new HighPass();

    //gravel
    private Noise gravel_noise = new Noise();
    private LowPass gravel_lop1 = new LowPass();
    private LowPass gravel_lop2 = new LowPass();
    private LowPass gravel_lop3 = new LowPass();
    private HighPass gravel_hip1 = new HighPass();
    private HighPass gravel_hip2 = new HighPass();
    private VoltageControlFilter gravel_vcf = new VoltageControlFilter();

    //wood
    private Noise wood_noise1 = new Noise();
    private Noise wood_noise2 = new Noise();
    private BandPass wood_bp1 = new BandPass();
    private BandPass wood_bp2 = new BandPass();
    private BandPass wood_bp3 = new BandPass();
    private BandPass wood_bp4 = new BandPass();
    private BandPass wood_bp5 = new BandPass();
    private BandPass wood_bp6 = new BandPass();
    private BandPass wood_bp7 = new BandPass();
    private BandPass wood_bp8 = new BandPass();

    ~Textures()
    {

    }

    //you know nothing, john snow
    public double snow(double input)
    {
        double output = 0;
        if (env1.perform(input) > .5)
        {
            double n1 = noise1.perform();
            double n2 = noise2.perform();
            lop1.setCutoff(50);
            lop2.setCutoff(70);
            lop3.setCutoff(10);
            double a = lop1.perform(n1) / lop2.perform(n1);
            double b = lop3.perform(n1) * 17;
            b *= b;
            b += .5;
            lop4.setCutoff(110);
            lop5.setCutoff(900);
            double c = lop4.perform(n2) / lop5.perform(n2);
            hip.setCutoff(300);
            double filterInput = hip.perform(clip(c * a * b, -1, 1));
            vcf.setQ(.5);
            double filterCenter = (input * 9000) + 700;
            double temp = vcf.perform_real(filterInput, filterCenter);
            output = (temp * input) * .2;
        }
        return output;
    }

    //sassy and grassy
    public double grass(double input)
    {
        double output = 0;
        if (env2.perform(input) > .5)
        {
            double n = grass_noise.perform();
            grass_hip1.setCutoff(2500);
            grass_lop1.setCutoff(300);
            grass_lop2.setCutoff(2000);
            grass_lop3.setCutoff(16);
            grass_vcf.setQ(1);
            grass_hip2.setCutoff(900);

            double a = grass_hip1.perform(grass_lop1.perform(n) / grass_lop2.perform(n));
            double filterInput = clip(((a * a * a * a) * 1e-05), -0.9, 0.9);
            double filterFreq = clip((grass_lop3.perform(n) * 23800) + 3400, 2000, 10000);
            double vcfOut = grass_vcf.perform_real(filterInput, filterFreq);
            double y = input * (grass_hip2.perform(vcfOut) * .3);

            double b = input * input * input * input;
            double c = clip(grass_osc.perform((b * 600) + 30), 0, .5);
            double x = (c * b) * .8;
            output = x + y;
        }

        return output;
    }

    //let's get dirty
    public double dirt(double input)
    {
        double output = 0;
        if (env3.perform(input) > .5)
        {
            dirt_lop.setCutoff(80);
            dirt_hip.setCutoff(200);
            double a = input * input * input * input;
            double x = a * dirt_osc1.perform((a * 500) + 40) * .5;
            double n = dirt_lop.perform(dirt_noise.perform()) * 70;
            double b = (input + .3) * n;
            double y = clip(dirt_hip.perform(dirt_osc2.perform((b * 70) + 70)), -1, 1) * .04;
            output = x + y;
        }

        return output;
    }

    //hit the gravel
    public double gravel(double input)
    {
        double output = 0;
        if (env4.perform(input) > .5)
        {
            gravel_lop1.setCutoff(300);
            gravel_lop2.setCutoff(2000);
            gravel_hip1.setCutoff(400);
            gravel_hip2.setCutoff(200);
            gravel_lop3.setCutoff(50);
            double n = gravel_noise.perform();
            double a = gravel_hip1.perform(gravel_lop1.perform(n) / gravel_lop2.perform(n));
            double filterInput = clip((a * a) * .01, -.9, .9);
            double b = (input * 1000) + (gravel_lop3.perform(n) * 50000);
            double filterFreq = clip(b, 500, 10000);
            double vcfOut = gravel_vcf.perform_real(filterInput, filterFreq);
            double y = gravel_hip2.perform(vcfOut) * 2;
            output = input * y;
        }
        return output;
    }

    //Norwegian wood
    public double wood(double input)
    {
        double output = 0;
        if (env5.perform(input) > .5)
        {
            wood_bp1.setCenterFrequency(95);
            wood_bp1.setQ(90);
            wood_bp2.setCenterFrequency(134);
            wood_bp2.setQ(90);
            wood_bp3.setCenterFrequency(139);
            wood_bp3.setQ(90);
            wood_bp4.setCenterFrequency(154);
            wood_bp4.setQ(90);

            double n1 = wood_noise1.perform();
            double x = (wood_bp1.perform(n1) + wood_bp2.perform(n1)
            + wood_bp3.perform(n1) + wood_bp4.perform(n1)) * 6;

            wood_bp5.setCenterFrequency(201);
            wood_bp5.setQ(90);
            wood_bp6.setCenterFrequency(123);
            wood_bp6.setQ(90);
            wood_bp7.setCenterFrequency(156);
            wood_bp7.setQ(90);
            wood_bp8.setCenterFrequency(189);
            wood_bp8.setQ(90);

            double n2 = wood_noise2.perform();
            double y = (wood_bp5.perform(n2) + wood_bp6.perform(n2)
            + wood_bp7.perform(n2) + wood_bp8.perform(n2)) * 8;

            double a = (Math.Sqrt((float)input) * x) * .5;
            double b = (((input * input) * 2) * y) * .6;

            output = a + b;
        }


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
        env1.Dispose();
        env2.Dispose();
        env3.Dispose();
        env4.Dispose();
        env5.Dispose();

        //Dispose snow
        noise1.Dispose();
        noise2.Dispose();
        lop1.Dispose();
        lop2.Dispose();
        lop3.Dispose();
        lop4.Dispose();
        lop5.Dispose();
        hip.Dispose();
        vcf.Dispose();

        //Dispose grass
        grass_noise.Dispose();
        grass_lop1.Dispose();
        grass_lop2.Dispose();
        grass_lop3.Dispose();
        grass_hip1.Dispose();
        grass_hip2.Dispose();
        grass_osc.Dispose();
        grass_vcf.Dispose();

        //Dispose dirt
        dirt_noise.Dispose();
        dirt_lop.Dispose();
        dirt_osc1.Dispose();
        dirt_osc2.Dispose();
        dirt_hip.Dispose();

        //Dispose gravel
        gravel_noise.Dispose();
        gravel_lop1.Dispose();
        gravel_lop2.Dispose();
        gravel_lop3.Dispose();
        gravel_hip1.Dispose();
        gravel_hip2.Dispose();
        gravel_vcf.Dispose();

        //Dispose wood
        wood_noise1.Dispose();
        wood_noise2.Dispose();
        wood_bp1.Dispose();
        wood_bp2.Dispose();
        wood_bp3.Dispose();
        wood_bp4.Dispose();
        wood_bp5.Dispose();
        wood_bp6.Dispose();
        wood_bp7.Dispose();
        wood_bp8.Dispose();

    }
}