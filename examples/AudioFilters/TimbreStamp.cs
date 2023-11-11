using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

public class TimbreStamp : MonoBehaviour
{

    private Convolution conv = new Convolution();//defaults to FFT Window 1024 and 4 overlap
    private Phasor phasor = new Phasor();
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private bool running = false;
    private double phasorInput = 0.0F;
    public int Squelch = 10;
    public double Frequency = 300.0F;
    

    void Start()
    {
        running = true;
    }

    ~TimbreStamp()
    {
        conv.Dispose();
        phasor.Dispose();
    }

    /*
     
     */
    public void runAlgorithm(double inputL, double inputR)
    {
        phasorInput = phasor.perform(Frequency);
        conv.setSquelch(Squelch);
        outputL = outputR = conv.perform(inputL, phasorInput);
    }

        void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {

            int i = 0;
            float in1 = data[n * channels + i];
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
