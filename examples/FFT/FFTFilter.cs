using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using PdPlusPlus;

[RequireComponent(typeof(AudioSource))]
public class FFTFilter : MonoBehaviour
{
    public int bin = 64;
    public float gain = 1.0F;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private const int fftWindowSize = 512;
    private rFFT rfft;
    private rIFFT rifft;
    private Oscillator osc = new Oscillator();
    private PdMaster pd = new PdMaster();
    private Noise noise = new Noise();
    private int overlap = 4;
    private double[] fft;
    private double[] hann;
    private ArrayList buffer = new ArrayList();
    private double[] inputBuffer;
    private double[] sum;
    private double[] ifft;
    private double[] ifftWas;
    private double[] filter;
    private double[] bins;
    private long sampleCounter = 0;
    private bool running = false;
    private long sr = 44100;

    // Start is called before the first frame update
    void Start()
    {
        pd.setFFTWindow(fftWindowSize);
        running = true;
        rfft = new rFFT(fftWindowSize);
        rifft = new rIFFT(fftWindowSize);
        filter = new double[fftWindowSize / 2];
        ifftWas = new double[fftWindowSize];
        bins = new double[fftWindowSize];
        ifft = new double[fftWindowSize];
        sum = new double[fftWindowSize];
        inputBuffer = new double[fftWindowSize / overlap];
        hann = new double[fftWindowSize];
        fft = new double[fftWindowSize];
        this.createHann();
        sr = AudioSettings.outputSampleRate;
        pd.setSampleRate(sr);
    }

    ~FFTFilter()
    {
        rfft.Dispose();
        rifft.Dispose();
        osc.Dispose();
        noise.Dispose();
    }

    
    public void runAlgorithm(double inputL, double inputR)
    {
        createFilter(bin);
        double input = noise.perform();
        double output = doFFT(input);
        outputL = outputR = output * gain;
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
            //float in1 = data[n * channels + i];
            float in1 = 0;
            this.runAlgorithm(in1, in1);
            while (i < channels)
            {   
                data[n * channels + i++] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
            }
            n++;
        }
    }

    public double doFFT(double input)
    {

        int hop = fftWindowSize / overlap;
        inputBuffer[(int)sampleCounter] = input;

        /* now for every overlap, or hop size, add our input to the end
          of the buffer.  This will add x new samples and reuse the 
          windowsize-x previous samples.  This is our overlap buffer.
        */

        if (sampleCounter == hop - 1)
        {
            //update our buffer
            for (int i = 0; i < hop; i++)
            {
                buffer.Add(inputBuffer[i]);             
                buffer.RemoveAt(0);
            }

            //Now we perform our FFT and multiply by our Hann window
            for (int i = 0; i < fftWindowSize; i++)
                fft = rfft.perform((double)buffer[i] * hann[i]);


            /*
            do something with our frequency bins here
            Remember with rFFT the first half of the array is real
            the back half is imaginary.
            In this example we are just applying a very broad linear band filter.
            */
            for (int i = 0, j = fftWindowSize - 1; i < fftWindowSize / 2; i++, j--)
            {
                double gain = filter[i];
                fft[i] *= gain;//real
                fft[j] *= gain;//imag

                double real = fft[i];
                double imag = fft[j];
                //sqrt( real^2 + imag^2) = freq bin magnitude
                double magnitude = Math.Sqrt((float)(real * real) + (float)(imag * imag));

                bins[i] = magnitude / 15;
            }

            //resynthesize our FFT block, multiply by our Hann window again
            for (int i = 0; i < fftWindowSize; i++)
                ifft[i] = rifft.perform(fft) * hann[i];

            /* Now we overlap our windows, and add them together
               Basically we add the last 3/4 of the previous to the
               first 3/4 of the current resynthesized block, then 
               just zeros at the end that will carry over to the next
               block.  Genius!
            */
            for (int i = 0; i < fftWindowSize; i++)
                sum[i] = ifft[i] + (i + hop < fftWindowSize ? ifftWas[i + hop] : 0);

            ifftWas = sum;

            sampleCounter = -1;
        }

        sampleCounter++;
        return sum[(int)sampleCounter] / (fftWindowSize * 1.5);//divide by 3N/2

    }

    /*
  Create a linear curve, you could replace this with any curve you like,
  x could be the center frequency of a band pass, or stop band or a 
  steep cutoff filter, etc.
  */
    public void createFilter(int x)
    {

        /*
        our filter curve is 1/2 our window because rFFT is half real and half imaginary
        similar to a Sasquatch...
        */
        
        for (int i = 0; i < filter.Length; i++)
        {
            if (x > filter.Length) x = filter.Length;

            if (i < x / 2)
                filter[i] = (double)i / filter.Length;
            else
                filter[i] = (double)(x - i) / filter.Length;

        }
    }

    /*
     We need to create a Hanning Window to smooth the FFT input
     You could change this to any other type of window function 
     such as:
     Blackmann
     Rectangular
     Hamming
     Nuttall
     Gaussian
     Tukey
     etc..
   */
    private void createHann()
    {

        createFilter(90);
        double winHz = 0;
        int windowSize = fftWindowSize;

        //clear our buffer first thing, it only does this once
        if (buffer.Count == 0)
        {
            double d = 0;
            for (int i = 0; i < fftWindowSize; i++)
                buffer.Add(d);
        }

        if (windowSize != 0)
        {
            winHz = sr / windowSize;
        }
        else
        {
            windowSize = 32;
            Debug.Log("Window size cannot be zero!");
        }

        osc.setPhase(0);
        for (int i = 0; i < windowSize; i++)
        {
            hann[i] = ((osc.perform(winHz) * -.5) + .5);
        }
        
    }
}
