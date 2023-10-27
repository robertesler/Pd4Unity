using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class ComplexFFT : IDisposable
{

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr cFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void cFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern IntPtr cFFT_perform0(IntPtr ptr, double real, double imaginary);
#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr cFFT_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void cFFT_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr cFFT_perform0(IntPtr ptr, double real, double imaginary);

    private IntPtr m_ComplexFFT;

    public ComplexFFT()
    {
        this.m_ComplexFFT = cFFT_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if (this.m_ComplexFFT != IntPtr.Zero)
        {
            cFFT_free0(this.m_ComplexFFT);
            this.m_ComplexFFT = IntPtr.Zero;
        }

        if (mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    ~ComplexFFT()
    {
        Dispose(false);
    }

    #region Wrapper Methods
    public IntPtr perform(double real, double imaginary)
    {
        return cFFT_perform0(this.m_ComplexFFT, real, imaginary);
    }


    #endregion Wrapper Methods
}