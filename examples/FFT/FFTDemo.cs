using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

[RequireComponent(typeof(AudioSource))]
public class FFTDemo : MonoBehaviour
{
    private int windowSize = 64;
    private rFFT rfft;
    private rIFFT rifft;
    private PdMaster pd = new PdMaster();
    private Oscillator osc = new Oscillator();
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private double[] fftBuffer;

    void Start()
    {
        rfft = new rFFT(windowSize);
        rifft = new rIFFT(windowSize);
        pd.setFFTWindow(windowSize); //We have to tell the dynamic library our FFT window size
        fftBuffer = new double[windowSize];
        running = true;
    }

    ~FFTDemo()
    {
        rfft.Dispose();
        rifft.Dispose();
        osc.Dispose();
    }

    private void runAlgorithm(double in1, double in2)
    {
        double sig = osc.perform(250);
        fftBuffer = rfft.perform(sig);
        double output = rifft.perform(fftBuffer)/windowSize;
        outputL = output;
        outputR = output;

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
            while (i < channels)
            {
                

                this.runAlgorithm(0,0);
                data[n * channels + i] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
            }
            n++;
        }
    }


}
