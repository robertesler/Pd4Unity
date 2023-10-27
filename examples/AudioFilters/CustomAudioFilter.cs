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
public class CustomAudioFilter : MonoBehaviour
{
    public double CenterFrequency = 500.0F;
    public double Q = 2.0F;
    public float gain = 0.2F;

    private double sampleRate = 0.0F;
    private bool running = false;

    MyFilter music = new MyFilter();

    void Start()
    {
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        running = true;
    }

    ~CustomAudioFilter()
    {
        music.Dispose();
    }

    void setCenterFrequency(double f1)
    {
        music.setCenterFrequency(f1);
    }

    void setQ(double q)
    {
        music.setQ(q);
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            music.setCenterFrequency(CenterFrequency);
            music.setQ(Q);

            int i = 0;
            while (i < channels)
            {
                float in1 = data[n * channels + i];
                float in2 = data[n * channels + i+1];
                music.runAlgorithm(in1, in2);
                data[n * channels + i++] += (float)music.outputL * gain;
                data[n * channels + i++] += (float)music.outputR * gain;
            }
            n++;
        }
    }
}