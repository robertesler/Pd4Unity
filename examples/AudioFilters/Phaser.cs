using System;
using UnityEngine;
using PdPlusPlus;
using static Chord;

[RequireComponent(typeof(AudioSource))]
public class Phaser : MonoBehaviour
{

    private Chord[] chord;
    private RealZeroReverse[] rzero_rev;
    private RealPole[] rpole;
    private HighPass hip = new HighPass();
    private Phasor phasor = new Phasor();
    private int max = 4;
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    public double frequency = 0;
    public double rate = .3;
    

    // Start is called before the first frame update
    void Start()
    {
        running = true;
        chord = new Chord[max];
        rzero_rev = new RealZeroReverse[max];
        rpole = new RealPole[max];
        hip.setCutoff(5);

        for (int i = 0; i < max; i++)
        {
            chord[i] = new Chord();
            rzero_rev[i] = new RealZeroReverse();
            rpole[i] = new RealPole();
        }
    }

    ~Phaser()
    {
        hip.Dispose();
        phasor.Dispose();

        for (int i = 0; i < max; i++)
        {
            chord[i].Dispose();
            rzero_rev[i].Dispose();
            rpole[i].Dispose();
        }
    }


    /*
  Here we use a 4 stage allpass and add the signal back into our chord.
  The filtered copy will cancel out frequencies from our sum
  and we get a classic phaser effect.
  */
    void runAlgorithm(double in1, double in2)
    {

        double c1 = chord[0].perform(frequency);
        double c2 = chord[1].perform(frequency * 1.333);
        double c3 = chord[2].perform(frequency * 1.5);
        double c4 = chord[3].perform(frequency * 2);
        double sum = hip.perform((c1 + c2 + c3 + c4)) * .2;
        double coef = getCoef(phasor.perform(rate));
        double rzero1 = rzero_rev[0].perform(sum, coef);
        double rpole1 = rpole[0].perform(rzero1, coef);
        double rzero2 = rzero_rev[1].perform(rpole1, coef);
        double rpole2 = rpole[1].perform(rzero2, coef);
        double rzero3 = rzero_rev[2].perform(rpole2, coef);
        double rpole3 = rpole[2].perform(rzero3, coef);
        double rzero4 = rzero_rev[3].perform(rpole3, coef);
        double rpole4 = rpole[3].perform(rzero4, coef);
        outputL = outputR = sum + rpole4; ;

    }

    //calculate our allpass coefficients
    double getCoef(double input)
    {
        double tri = Math.Abs((float)input - .5f);//phasor input converted to triangle wave
        double ph = 1 - .03 - (0.6 * tri * tri); //set range
        return ph;
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
