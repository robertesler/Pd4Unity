using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using PdPlusPlus;

public class TimeCompression : MonoBehaviour
{
    public float Transposition = -20.0F;
    public float ChunkSize = 25.0F;
    public float Precession = 60.0F;
    public float LoopLength = 900.0F;
    public AudioClip audioFile;
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private TabRead4 tabread1 = new TabRead4();
    private TabRead4 tabread2 = new TabRead4();
    private SampleHold samphold1 = new SampleHold();
    private SampleHold samphold2 = new SampleHold();
    private Cosine cos1 = new Cosine();
    private Cosine cos2 = new Cosine();
    private Phasor phasor1 = new Phasor();
    private Phasor phasor2 = new Phasor();
    private HighPass hip = new HighPass();
    private SoundFiler soundfiler = new SoundFiler();
    private float transposition = -20;
    private float chunkSize = 25;
    private float precession = 60;
    private float loopLength = 900;
    private float readPoint = 0;
    private double[] loop;
    private int fileSize;
    private float phase1 = 0;
    private float phase2 = 0;
    private long sr = 44100;
    private long counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        string tempFile = AssetDatabase.GetAssetPath(audioFile);//our file's path
        fileSize = (int)soundfiler.read(tempFile);
       
        loop = new double[fileSize + 4];
        loop = soundfiler.getArray(fileSize);
        tabread1.setTable(loop, fileSize);
        tabread2.setTable(loop, fileSize);
        tabread1.setSampleRate(sr);
        tabread2.setSampleRate(sr);
        running = true;
        setChunkSize(25);
        setPrecession(60);
        setLoopLength(900);
        setTransposition(-20);

    }

    private void Update()
    {
        setPrecession(Precession);
        setLoopLength(LoopLength);
        setTransposition(Transposition);
        setChunkSize(ChunkSize);
    }

    private float wrap(float input)
    {
        float output = 0;
        int k;
        float f = input;
        if (f > int.MaxValue || f < int.MinValue)
            f = 0;
     
        k = (int)f;
        if (k <= f)
         output = f - k;
        else
         output = f - (k - 1);

        return output;
    }

    ~TimeCompression()
    {
        tabread1.Dispose();
        tabread2.Dispose();
        samphold1.Dispose();
        samphold2.Dispose();
        cos1.Dispose();
        cos2.Dispose();
        phasor1.Dispose();
        phasor2.Dispose();
        hip.Dispose();
        soundfiler.Dispose();
    }



    public void runAlgorithm(double inputL, double inputR)
    {
        
        //We have our first read point and phase here
        readPoint = (float)phasor1.perform(getPrecession() / getLoopLength()) * getLoopLength();
        phase1 = (float)phasor2.perform(getTransposition());
        phase2 = wrap((float)(phase1 + .5F));
        double first = samphold1.perform(getChunkSize(), phase1) * phase1;
        first += readPoint;
        first = (first * sr) + 1;
        double table1 = tabread1.perform(first) * cos1.perform((phase1 - .5F) * .5F);
        //Our second read point and second phase
        double second = samphold2.perform(getChunkSize(), phase2) * phase2;
        second += readPoint;
        second = (second * sr) + 1;
        double table2 = tabread2.perform(second) * cos2.perform((phase2 - .5F) * .5F);
        hip.setCutoff(5);//Reduce DC
        outputL = outputR = hip.perform(table1 + table2); //add each phase together
        
    }

    private void reset()
    {
        setChunkSize(25);
        setPrecession(60);
        setLoopLength(900);
        setTransposition(-20);
    }

    private void setChunkSize(float cs)
    {
        chunkSize = cs / 1000.0F;//msec
    }

    private float getChunkSize()
    {
        return chunkSize;
    }

    private void setPrecession(float p)
    {
        precession = p / 100.0F;//in %
    }

    private float getPrecession()
    {
        return precession;
    }
    //(2^t/120 - p)/c
    private void setTransposition(float f1)
    {
        transposition = ( (float)Math.Pow(2, f1 / 120.0F) - getPrecession() ) / getChunkSize();

    }

    private float getTransposition()
    {
        return transposition;
    }

    private void setLoopLength(float l)
    {
        loopLength = l / 1000.0F;//msec 
    }

    private float getLoopLength()
    {
        return loopLength;//also in msec
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
