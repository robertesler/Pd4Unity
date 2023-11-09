using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

[RequireComponent(typeof(AudioSource))]
public class SSB : MonoBehaviour
{
    private BiQuad biquad1 = new BiQuad();
    private BiQuad biquad2 = new BiQuad();
    private BiQuad biquad3 = new BiQuad();
    private BiQuad biquad4 = new BiQuad();
    private Phasor phasor = new Phasor();
    private Oscillator osc = new Oscillator();
    private Cosine cosLeft = new Cosine();
    private Cosine cosRight = new Cosine();
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private int sr;
    public float freqShift = 0;//range is about -100 to +100

    // Start is called before the first frame update
    void Start()
    {
        /*
      According to H09.ssb.modulation.pd this creates a pair of allpass
      filters that shift the input by 90 degrees, making them suitable
      real and imaginary pairs.
    */
        biquad1.setCoefficients(1.94632, -0.94657, 0.94657, -1.94632, 1);
        biquad2.setCoefficients(0.83774, -0.06338, 0.06338, -0.83774, 1);
        biquad3.setCoefficients(-0.02569, 0.260502, -0.260502, 0.02569, 1);
        biquad4.setCoefficients(1.8685, -0.870686, 0.870686, -1.8685, 1);
        running = true;
        sr = AudioSettings.outputSampleRate;
    }

    ~SSB()
    {
        biquad1.Dispose();
        biquad2.Dispose();
        biquad3.Dispose();
        biquad4.Dispose();
        phasor.Dispose();
        osc.Dispose();
        cosLeft.Dispose();
        cosRight.Dispose();
    }

    //This is your DSP chain, run everything audio from here
    public void runAlgorithm(double inputL, double inputR)
    {
        /*
      We run our allpass filters (see below) in series, this is also 
      called a Hilbert transform
    */
        double input = (inputL + inputR) * .5; //convert to mono
        double hilbertRight = biquad2.perform(biquad1.perform(input));
        double hilbertLeft = biquad4.perform(biquad3.perform(input));

        /*
          This part is also called complex modulation, similar to ring modulation
          but only shifts one set of frequencies instead of a positive and negative
        */
        double phase = phasor.perform(freqShift);
        double cos = cosLeft.perform(phase);
        double sine = cosRight.perform(phase + -.25);

        double output = (hilbertLeft * cos) - (hilbertRight * sine);
        outputL = outputR = output;
    }

    //our audio loop, it is apparently subject to garbage collection, so there can be glitches.
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            int i = 0;
            float in1 = data[n * channels + i];//this is our input from the Audio Source if you want to use it. 
            this.runAlgorithm(in1, in1);
            while (i < channels)
            {
                
                data[n * channels + i] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
            }
            n++;
        }
    }
}
