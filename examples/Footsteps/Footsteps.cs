using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Foot;

/*
 * This is based on Andy Farnell's "Designing Sound" example.
 * There are five textures: snow, grass, dirt, gravel, wood
 * See Textures.cs for more...
 */
public class Footsteps : MonoBehaviour
{

    Foot foot = new Foot();
    public float Speed = 0;
    public int Texture = 0;
    public double Gain = 1.0F;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private double sampleRate = 0.0F;
    private bool running = false;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        running = true;

    }

    ~Footsteps()
    {
        foot.Dispose();
    }

    //All DSP code goes here
    private void runAlgorithm(double in1, double in2)
    {
        //right now we only have 5 textures
        if (Texture > 4) Texture = 4;
        if (Texture < 0) Texture = 0;
        outputL = outputR = foot.perform(Speed, Texture) * Gain;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            this.runAlgorithm(0, 0);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i++] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
            }
            n++;
        }
    }


}
