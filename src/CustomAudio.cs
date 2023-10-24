using UnityEngine;
using static MyMusic;

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
public class AudioTest : MonoBehaviour
{
    public double frequency = 400.0F;
    public double frequency2 = 2.0F;
    public float gain = 0.2F;

    private double sampleRate = 0.0F;
    private bool running = false;

    MyMusic music = new MyMusic();

    void Start()
    {
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        running = true;
    }

    ~AudioTest()
    {
        music.Dispose();
        Debug.Log("Free Memory");
    }

    void setFreq1(double f1)
    {
        music.setFreq1(f1);
    }

    void setFreq2(double f2)
    {
        music.setFreq2(f2);
    }



    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            music.setFreq1(frequency);
            music.setFreq2(frequency2);
            music.setGain(gain);
            music.runAlgorithm(0, 0);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i++] += (float)music.outputL;
                data[n * channels + i++] += (float)music.outputR;
                //i++;
            }
            n++;
        }
    }
}