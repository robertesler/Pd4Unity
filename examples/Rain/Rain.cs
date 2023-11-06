using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;
using static GlassWindow;
using static Drop;

/*
 This is an example of a rain generator.
 The Rain Intensity ranges from (.001 - 1+) high - low (inverse proportions)
 The Rain Volume is just a gain factor.
 The Bp Cf and Q will control the far field rain, which is just filtered noise.
 This example is inspired by Andy Farnell's "Designing Sound" book.
 There is also a comparative example in the Pd4P3 Processing libary.
 https://github.com/robertesler/Pd4P3/blob/main/examples/Nature/Rain/Rain.pde
 */

[RequireComponent(typeof(AudioSource))]
public class Rain : MonoBehaviour
{
    public double RainIntensity = 0.06F;
    public double RainVolume = 3.0F;
    public double bpCf = 1400;
    public double bpQ = 2.0F;
    public double bpGain = 1.0F;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private Noise noise = new Noise();
    private Noise noise2 = new Noise();
    private BandPass bp = new BandPass();
    private HighPass hip = new HighPass();
    private LowPass lop = new LowPass();
    private Delay del = new Delay();
    private Drop drop = new Drop();
    private GlassWindow window = new GlassWindow();
    private double delread = 0;
    private double bpRainVol = 2.8;
    private bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        del.setDelayTime(300);
        hip.setCutoff(9000);
        lop.setCutoff(500);
        bp.setCenterFrequency(bpCf);
        bp.setQ(bpQ);
        running = true;
    }

    ~Rain()
    {
        noise.Dispose();
        noise2.Dispose();
        hip.Dispose();
        bp.Dispose();
        lop.Dispose();
        del.Dispose();
        drop.Dispose();
        window.Dispose();
    }


    //All DSP code goes here
    private void runAlgorithm(double in1, double in2)
    {
        outputL = outputR = 0;
        double dropInput = (del.perform(delread) * 24) + 6;
        double n = noise.perform();
        double farFieldRain = bp.perform(n) * (bpRainVol * .01);
        double x = drop.perform(n, dropInput, RainIntensity, RainVolume);
        double win = hip.perform(x) * 20;
        delread = window.perform(win);
        outputR = lop.perform(delread) + (farFieldRain * bpGain);
        outputL = outputR;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            this.runAlgorithm(0, 0);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i++] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
            }
            n++;
        }
    }


}
