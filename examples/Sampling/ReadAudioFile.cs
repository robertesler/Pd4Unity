using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PdPlusPlus;

public class ReadAudioFile : MonoBehaviour
{
    public AudioClip audioFile;
    public bool start = false;
    private ReadSoundFile readsf = new ReadSoundFile();
    private bool running = false;
    private int ch = 2;
    private double outputL = 0.0F;
    private double outputR = 0.0F;

    // Start is called before the first frame update
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
        
        if(start)
        {
            bool t;
            t = readsf.start();
            Debug.Log(t);
        }  
        else
        {
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
