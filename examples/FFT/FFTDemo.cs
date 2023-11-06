using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PdPlusPlus;

[RequireComponent(typeof(AudioSource))]
public class FFTDemo : MonoBehaviour
{
    public double frequency = 250.0F;
    private int windowSize = 256;
    private rFFT rfft;
    private rIFFT rifft;
    private cFFT fft;
    private cIFFT ifft;
    private PdMaster pd = new PdMaster();
    private Oscillator osc = new Oscillator();
    private Oscillator osc2 = new Oscillator();
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private double[] rfftBuffer;
    private double[] cfftBuffer;
    private double[] fftComplex = new double[2];//holds our real and imaginary values

    void Start()
    {
        pd.setFFTWindow(windowSize); //We have to tell the dynamic library our FFT window size
        rfft = new rFFT(windowSize);
        rifft = new rIFFT(windowSize);
        fft = new cFFT(windowSize);
        ifft = new cIFFT(windowSize);
        rfftBuffer = new double[windowSize];//real and imaginary are copied to each half of buffer.
        cfftBuffer = new double[windowSize * 2];//complex FFT requires a buffer 2 * fftWindowSize, real and imaginary.
        running = true;
    }

    ~FFTDemo()
    {
        rfft.Dispose();
        rifft.Dispose();
        fft.Dispose();
        ifft.Dispose();
        osc.Dispose();
        osc2.Dispose();
    }

    private void runAlgorithm(double in1, double in2)
    {
        //our Real FFT
        double sig = osc.perform(frequency);
        rfftBuffer = rfft.perform(sig);//real FFT
        double fftReal = rifft.perform(rfftBuffer); //real returns one value

        //our Complex FFT
        double sig2 = osc2.perform(frequency*1.5);
        cfftBuffer = fft.perform(sig2, 0);//complex FFT
        fftComplex = ifft.perform(cfftBuffer);//complex returns a real and imaginary value
        
        outputL = (fftReal / windowSize) * .5; //real FFT
        outputR = (fftComplex[0] / windowSize) * .5; //complex FFT

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
