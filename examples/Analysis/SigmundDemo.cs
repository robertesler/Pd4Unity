using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

[RequireComponent(typeof(AudioSource))]
public class SigmundDemo : MonoBehaviour
{
    public double frequency = 0.0F;
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private int sr;
    private Sigmund sigmund;//defaults to pitch and env
    private int peaks = 10;
    private Oscillator osc = new Oscillator();

    // Start is called before the first frame update
    void Start()
    {
        sigmund = new Sigmund("peaks", peaks);
        sr = AudioSettings.outputSampleRate;
        running = true;
    }

    ~SigmundDemo()
    {
        sigmund.Dispose();
        osc.Dispose();
    }

    //This is your DSP chain, run everything audio from here
    public void runAlgorithm(double inputL, double inputR)
    {
        double x = osc.perform(frequency);
        sigmund.perform(x);
        //double p = sigmund.pitch;
        //double e = sigmund.envelope;
        
        double[] peakArray = new double[peaks * 5]; //peaks gives 5 numbers
        peakArray = sigmund.output;

        for(int i = 0; i < peakArray.Length; i++)
        {
            
                Debug.Log(peakArray[i]);
        }
       
        outputL = 0;
        outputR = 0;
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
            //float in1 = data[n * channels + i];//this is our input from the Audio Source if you want to use it. 
            this.runAlgorithm(0, 0);
            while (i < channels)
            {
                data[n * channels + i++] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
            }
            n++;
        }
    }
}
