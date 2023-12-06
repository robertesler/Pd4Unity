using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FireGen;
using PdPlusPlus;

public class Fire : MonoBehaviour
{

    FireGen fire1 = new FireGen();
    FireGen fire2 = new FireGen();
    FireGen fire3 = new FireGen();
    FireGen fire4 = new FireGen();
    BandPass bp1 = new BandPass();
    BandPass bp2 = new BandPass();
    BandPass bp3 = new BandPass();
    HighPass hip = new HighPass();
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;

    //Free all objects created from Pd4Unity lib
    ~Fire()
    {
        fire1.Dispose();
        fire2.Dispose();
        fire3.Dispose();
        fire4.Dispose();
        bp1.Dispose();
        bp2.Dispose();
        bp3.Dispose();
        hip.Dispose();
    }

    void Start()
    {
        //set our filters
        bp1.setCenterFrequency(600);
        bp1.setQ(0.2);

        bp2.setCenterFrequency(1200);
        bp2.setQ(0.7);

        bp3.setCenterFrequency(2600);
        bp3.setQ(0.4);

        hip.setCutoff(1000);
        running = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //We filter and sum together four fire generators
    void runAlgorithm(double in1, double in2)
    {

        double fire = bp1.perform(fire1.perform()) + bp2.perform(fire2.perform()) +
        bp3.perform(fire3.perform()) + hip.perform(fire4.perform());

        outputL = outputR = fire * .3;

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
