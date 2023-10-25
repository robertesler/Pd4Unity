using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class LowPass : IDisposable
{

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr LowPass_allocate0();

    [DllImport("__Internal")]
    public static extern void LowPass_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double LowPass_perform0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern void LowPass_setCutOff0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern double LowPass_getCutoff0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void LowPass_clear0(IntPtr ptr, double q);
#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr LowPass_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void LowPass_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double LowPass_perform0(IntPtr ptr, double input);

    [DllImport("pdplusplusUnity")]
    public static extern void LowPass_setCutOff0(IntPtr ptr, double f);

    [DllImport("pdplusplusUnity")]
    public static extern double LowPass_getCutoff0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void LowPass_clear0(IntPtr ptr, double q);
#endif

    private IntPtr m_LowPass;

    public LowPass()
    {
        this.m_LowPass = LowPass_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if (this.m_LowPass != IntPtr.Zero)
        {
            LowPass_free0(this.m_LowPass);
            this.m_LowPass = IntPtr.Zero;
        }

        if (mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    ~LowPass()
    {
        Dispose(false);
    }

    #region Wrapper Methods
    public double perform(double freq)
    {
        return LowPass_perform0(this.m_LowPass, freq);
    }

    public void setCutOff(double f)
    {
        LowPass_setCutOff0(this.m_LowPass, f);
    }

    public double getCutOff()
    {
        LowPass_getCutoff0(this.m_LowPass);
    }

    public void clear(double q)
    {
        LowPass_clear0(this.m_LowPass, q);
    }

    #endregion Wrapper Methods
}