using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaterGen;
/*
 This a basic water generator based on Andy Farnell's book "Designing Sound". 
 */
public class Water : MonoBehaviour
{
    public double gain = 1.0F;
    public double rate = 1.0F;
    WaterGen water1;
    WaterGen water2;
    WaterGen water3;
    WaterGen water4;
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;

    ~Water()
    {
        water1.Dispose();
        water2.Dispose();
        water3.Dispose();
        water4.Dispose();
    }

    void Start()
    {
        running = true;
        long sr = AudioSettings.outputSampleRate;
        water1 = new WaterGen(sr);
        water2 = new WaterGen(sr);
        water3 = new WaterGen(sr);
        water4 = new WaterGen(sr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * You can experiment here to get different results.
     * 
     * */
    void runAlgorithm(double in1, double in2)
    {
        double w1 = water1.perform(8 * rate, 4000, 150, 2.689);
        double w2 = water2.perform(11 * rate, 5000, 100, 2.1);
        double w3 = water3.perform(5 * rate, 2000, 100, 1.897);
        double w4 = water4.perform(13 * rate, 6000, 180, 3);
        outputL = (w1 + w2) * gain;
        outputR = (w3 + w4) * gain;

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
