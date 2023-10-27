using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class RealIFFT : IDisposable
{

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr rIFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void rIFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double rIFFT_perform0(IntPtr ptr, double* input;
#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr rIFFT_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void rIFFT_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double rIFFT_perform0(IntPtr ptr, double* input);

    private IntPtr m_RealIFFT;

    public RealIFFT()
    {
        this.m_RealIFFT = rIFFT_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if (this.m_RealIFFT != IntPtr.Zero)
        {
            rIFFT_free0(this.m_RealIFFT);
            this.m_RealIFFT = IntPtr.Zero;
        }

        if (mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    ~RealIFFT()
    {
        Dispose(false);
    }

    #region Wrapper Methods
    public double perform(double[] input)
    {
        return rIFFT_perform0(this.m_RealIFFT, input);
    }


    #endregion Wrapper Methods
}