using UnityEngine;
using PdPlusPlus;

/*
 This is a basic music generator.  There is a scale
in an array and gets randomly chosen.  See the details
on how I dealt with rhythm.
This is very simple, including the synth, like I'm going
to give you the good stuff. : )
Anyway, look at the adsr() function for one way to envelope
a synthesizer.  For this demo, the synth is just a sine wave.
 */

public class MusicGenerator : MonoBehaviour
{
    public int StartingNote = 60;
    public float gain = .5F;
    private double tempo = 120;
    private int prevNote = 0;
    private Metro metro = new Metro();
    private Oscillator osc = new Oscillator();
    private Line line = new Line();
    private Noise noise = new Noise();
    double freq = 0;
    private int scaleLength = 12;
    private int [] scale;
    private int[] intervals = {-5, -3, -2, 0, 2, 3, 5, 7, 8, 10, 12, 14};
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private double lineFloat = 0;
    private bool start = false;

    void Start()
    {
        scale = new int[scaleLength];
        running = true;
        setScale(StartingNote);
        metro.setBPM(true);//converts ms to BPM
    }


    void Update()
    {
        if(prevNote != StartingNote)
        {
            setScale(StartingNote);
            prevNote = StartingNote;
        }
       
       
    }

    ~MusicGenerator()
    {
        metro.Dispose();
        osc.Dispose();
        line.Dispose();
        noise.Dispose();
    }

    public void runAlgorithm(double inputL, double inputR)
    {
        bool go = metro.perform(tempo);
        if (go) freq = getScaleNote();

        if (freq == 0)
            outputL = outputR = 0;
        else
             outputL = outputR = osc.perform(freq) * gain * adsr(go, 1);
    }

    private double adsr(bool bang, float attack)
    {
        if (bang)
            start = true;

        if (lineFloat == attack)
            start = false;

        if (start)
        {
            lineFloat = line.perform(attack, 20);
        }
        else
        {
            lineFloat = line.perform(0, 200);
        }

        return lineFloat;
    }

    private void setScale(int note)
    {
        for(int i = 0; i < scaleLength; i++)
        {
            scale[i] = note + intervals[i];
        }
    }

    private double getScaleNote()
    {
        double freq = 0;

        /*
         * this acts as a random number generator, 
         * the + 4 is so we have some gaps in the rhythm
         * */

        double rand = ((noise.perform() * .5) + .5) * (scaleLength + 4);
        int index = (int)rand;

        //int index = 4;
        if (index >= scaleLength)
        {
            return 0;
        }
        freq = metro.mtof(scale[index]);
        
        return freq;
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
