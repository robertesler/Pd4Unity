using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

[RequireComponent(typeof(AudioSource))]
public class WindNative : MonoBehaviour, IDisposable {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Wind_allocate0();

    [DllImport("__Internal")]
    public static extern void Wind_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern IntPtr Wind_perform0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void Wind_setWind0(IntPtr ptr, double f1);
#else
    [DllImport("pdplusplusUnity")]
    public static extern IntPtr Wind_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void Wind_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr Wind_perform0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void Wind_setWind0(IntPtr ptr, double f1);

#endif
    // Start is called before the first frame update
    public double WindFrequency = 0.25;
    public double Gain = 1.0F;
    private double sampleRate = 0.0F;
    private bool running = false;
    private double outputL = 0.0F;
    private double outputR = 0.0F;
    private IntPtr m_buffer;

    void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        running = true;

    }

    private IntPtr m_WindNative;

    public WindNative()
    {
        this.m_WindNative = Wind_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if (this.m_WindNative != IntPtr.Zero)
        {
            Wind_free0(this.m_WindNative);
            this.m_WindNative = IntPtr.Zero;
        }

        if (mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    #region Wrapper Methods
    ~WindNative()
    {
        Dispose(false);
    }

    public IntPtr perform()
    {
        return Wind_perform0(this.m_WindNative);
    }
    
   public void setWind(double freq)
    {
        Wind_setWind0(this.m_WindNative, freq);
    }
    #endregion Wrapper Methods

    public void runAlgorithm()
    {
        setWind(WindFrequency);
        this.m_buffer = perform();
       // long size = Marshal.ReadInt64(this.m_buffer);
        double[] buf = new double[2];
        Marshal.Copy(this.m_buffer, buf, 0, buf.Length);
        outputL = buf[0] * Gain;
        outputR = buf[1] * Gain;
        
        
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
            while (i < channels)
            {
                this.runAlgorithm();
                data[n * channels + i++] = (float)this.outputL;
                data[n * channels + i++] = (float)this.outputR;
                //i++;
            }
            n++;
        }
    }

}
