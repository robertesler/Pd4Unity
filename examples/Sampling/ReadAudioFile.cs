using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PdPlusPlus;

/*
 This example streams an audio file from disk.  It essentially
does the same thing as Audio Source and/or Audio Clip.  So I only
recommend using it if those cannot do what you need.
See the note in ReadSoundFile.cs about performance issues.

I instead recommend using SoundFiler to load audio files into
RAM.  
 */

public class ReadAudioFile : MonoBehaviour
{
    public AudioClip audioFile;
    public bool start = false;
    private ReadSoundFile readsf = new ReadSoundFile();
    private bool running = false;
    private int channels = 1;
    private double outputL = 0.0F;
    private double outputR = 0.0F;

    // Call open() before calling start.  
    void Start()
    {
        string tempFile = AssetDatabase.GetAssetPath(audioFile);
        readsf.setBufferSize(10);
        readsf.open(tempFile , 0);
        running = true;
    }

    ~ReadAudioFile()
    {
        readsf.Dispose();
    }

   
    public void runAlgorithm(double inputL, double inputR)
    {
        //This is where we stream the data from disk.  
        if(start && !readsf.isComplete())
        {
                double[] output;
                output = readsf.start();
                outputL = outputR = output[0];
            
        }  
        else
        {
            if(readsf.isComplete())
            {
                start = false;
                readsf.stop();
            }
            outputL = outputR = 0;
        }
       
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
