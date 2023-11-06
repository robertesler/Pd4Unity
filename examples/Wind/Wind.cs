using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static WindGen;

[RequireComponent(typeof(AudioSource))]
public class Wind : MonoBehaviour
{
    public double WindFrequency = 0.25F;
    public double Gain = 1.0F;
    private WindGen windGen = new WindGen();
    private double [] windOut = new double[2];
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private double sampleRate = 0.0F;
    private bool running = false;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        running = true;

    }

   
    ~Wind()
    {
        windGen.Dispose();
    }

    //All DSP code goes here
    private void runAlgorithm(double in1, double in2)
    {

        windGen.setWindFreq(WindFrequency);
        windOut = windGen.perform();
        outputL = windOut[0] * Gain;
        outputR = windOut[1] * Gain;

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
