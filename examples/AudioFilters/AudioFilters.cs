using UnityEngine;
using static ResonantFilter;
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
public class AudioFilters : MonoBehaviour
{
    public double CenterFrequency = 500.0F;
    public double Q = 2.0F;
    public float gain = 0.2F;
    public float BobRes = 1.0F;
    public bool BandPass = false;
    public bool LowPass = false;
    public bool HighPass = false;
    public bool VCF = false;
    public bool Bob = false;
    public bool SlewLop = false;
    public bool Res = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private double sampleRate = 0.0F;
    private double bpFade = 0.0F;
    private double lopFade = 0.0F;
    private double hipFade = 0.0F;
    private double vcfFade = 0.0F;
    private double bobFade = 0.0F;
    private double slopFade = 0.0F;
    private double resFade = 0.0F;
    private double vcfOutput;
    private bool running = false;
    private BandPass bp = new BandPass();
    private LowPass lop = new LowPass();
    private HighPass hip = new HighPass();
    private VoltageControlFilter vcf = new VoltageControlFilter();
    private BobFilter bob = new BobFilter();
    private SlewLowPass slop = new SlewLowPass();
    private ResonantFilter res;
    private Noise noise = new Noise();
    private Line line1 = new Line();
    private Line line2 = new Line();
    private Line line3 = new Line();
    private Line line4 = new Line();
    private Line line5 = new Line();
    private Line line6 = new Line();
    private Line line7 = new Line();

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        res = new ResonantFilter((int)sampleRate);
        running = true;
    }

    ~AudioFilters()
    {
        bp.Dispose();
        lop.Dispose();
        hip.Dispose();
        vcf.Dispose();
        bob.Dispose();
        slop.Dispose();
        res.Dispose();
        noise.Dispose();
        line1.Dispose();
        line2.Dispose();
        line3.Dispose();
        line4.Dispose();
        line5.Dispose();
        line6.Dispose();
        line7.Dispose();
    }

    public void runAlgorithm(double inputL, double inputR)
    {
        
        double bpOut = 0;
        double lopOut = 0;
        double hipOut = 0;
        double vcfOut = 0;
        double bobOut = 0;
        double slopOut = 0;
        double resOut = 0;
        double time = 200;

        //our bandpass
        bp.setCenterFrequency(CenterFrequency);
        bp.setQ(Q);
        bpOut = bp.perform((inputL + inputR) ) * gain * line1.perform(bpFade, time);

        //our low pass
        lop.setCutoff(CenterFrequency);
        lopOut = lop.perform(inputL + inputR) * gain * line2.perform(lopFade, time);

        //our high pass
        hip.setCutoff(CenterFrequency);
        hipOut = hip.perform(inputL + inputR) * gain * line3.perform(hipFade, time);

        //our voltage control filter
        vcf.setQ(Q);
        vcfOutput = vcf.perform_real(inputL + inputR, CenterFrequency);
        vcfOut = vcfOutput * gain * line4.perform(vcfFade, time);

        //our Bob Moog filter
        bob.setCutoffFrequency(CenterFrequency);
        bob.setResonance(BobRes);
        bobOut = bob.perform(inputL + inputR) * gain * line5.perform(bobFade, time);

        //our slew low pass
        slopOut = slop.perform(inputL + inputR, CenterFrequency, .2, 27, .36, 0) * gain * line6.perform(slopFade, time);

        //our resonant filter
        res.setFreq(CenterFrequency);
        res.setQ(Q);
        resOut = res.perform(inputL, inputR) * gain * line7.perform(resFade, time);

        //We will use our Line class to fade in or out each filter type
        if (BandPass)
        {
            bpFade = 1;
        }
        else
        {
            bpFade = 0;
        }

        if (LowPass)
        {
            lopFade = 1;
        }
        else
        {
            lopFade = 0;
        }

        if (HighPass)
        {
            hipFade = 1;
        }
        else
        {
            hipFade = 0;
        }

        if (VCF)
        {
            vcfFade = 1;
        }
        else
        {
            vcfFade = 0;
        }

        if(Bob)
        {
            bobFade = 1;
        }
        else
        {
            bobFade = 0;
        }

        if(SlewLop)
        {
            slopFade = 1;
        }
        else
        {
            slopFade = 0;
        }

        if(Res)
        {
            resFade = 1;
        }
        else
        {
            resFade = 0;
        }
        outputL = outputR = bpOut + lopOut + hipOut + vcfOut + bobOut + slopOut + resOut;
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