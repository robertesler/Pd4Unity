using UnityEngine;
using PdPlusPlus;

/*
OnAudioFilterRead is called every time a chunk of audio is sent to the filter (this happens frequently, every ~20ms depending on the sample rate and platform).
The audio data is an array of floats ranging from [-1.0f;1.0f] and contains audio from the previous filter in the chain or the AudioClip on the AudioSource.
If this is the first filter in the chain and a clip isn't attached to the audio source, this filter will be played as the audio source.
In this way you can use the filter as the audio clip, procedurally generating audio.

If there is more than one channel, the channel data is interleaved. This means each consecutive data sample in the array comes from a different channel until you
run out of channels and loop back to the first. data.Length reports the total size of the data, so to find the number of samples per channel, divide data.Length by channels.

If OnAudioFilterRead is implemented a VU meter is shown in the Inspector displaying the outgoing sample level.
The process time of the filter is also measured and the spent milliseconds are shown next to the VU meter.
The number turns red if the filter is taking up too much time, meaning the mixer will be starved of audio data. 
 
 */

[RequireComponent(typeof(AudioSource))]
public class OscillatorTest : MonoBehaviour
{
    public double frequency = 400.0F;
    public float gain = 0.2F;
    public bool Sine = true;
    public bool Square = false;
    public bool Sawtooth = false;
    public bool Noise = false;
    private double outputL = 0;
    private double outputR = 0;
    private double sineFade = 0;
    private double squareFade = 0;
    private double sawFade = 0;
    private double noiseFade = 0;
    private Noise noise = new Noise();
    private Oscillator osc = new Oscillator();
    private Oscillator osc2 = new Oscillator();
    private Phasor phasor = new Phasor();
    private Line line1 = new Line();
    private Line line2 = new Line();
    private Line line3 = new Line();
    private Line line4 = new Line();

    private double sampleRate = 0.0F;
    private bool running = false;

    
    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        running = true;

    }

    //For C# we need to call Dispose for every Pd4Unity object.
    ~OscillatorTest()
    {
        osc.Dispose();
        osc2.Dispose();
        noise.Dispose();
        phasor.Dispose();
        line1.Dispose();
        line2.Dispose();
        line3.Dispose();
        line4.Dispose();
    }
    /*
     This is where we will put all of signal processing.  You can access the loop via
     public members of getter/setter functions from other classes.
    The syntax is almost exactly that of other forms of Pd++ (e.g. Pd4P3 in Processing)
    So these can be easily adapated across platforms.
     */
    private void runAlgorithm(double inputL, double inputR)
    {
        outputL = outputR = 0.0F;
        double sineOut = 0.0F;
        double sqOut = 0.0F;
        double sawOut = 0.0F;
        double noiseOut = 0.0F;
        double time = 200;

        // our sine wave
        sineOut = (float)osc.perform(frequency) * gain * line1.perform(sineFade, time);

        //our square wave
        double sq = (float)osc2.perform(frequency);
        if (sq > 0)
        {
            sq = 1;
        }
        else
        {
            sq = -1;
        }
        sqOut = sq * gain * line2.perform(squareFade, time) * .5;

        //our sawtooth
        sawOut = (float)phasor.perform(frequency) * gain * line3.perform(sawFade, time);

        //our noise
        noiseOut = (float)noise.perform() * gain * line4.perform(noiseFade, time);

        //We will use our Line class to fade in or out each oscillator type
        if (Sine)
        {
            sineFade = 1;
        }
        else
        {
            sineFade = 0;
        }
       
        if(Square)
        {
            squareFade= 1;
        }
        else
        {
            squareFade = 0;
        }

        if(Sawtooth)
        {
            sawFade = 1;
        }
        else
        {
            sawFade = 0;
        }

        if(Noise)
        {
            noiseFade = 1;
        }
        else
        {
            noiseFade = 0;
        }

        //We add our oscillators together.  This could distort if you run all at the same time with the gain higher than .25;
        outputL = outputR = sineOut + sawOut + sqOut + noiseOut;
    }

    /*
     This is our audio loop. 
     */

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
                //i++;
            }
            n++;
        }
    }
}
