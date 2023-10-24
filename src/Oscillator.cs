using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class Oscillator : IDisposable
{

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Oscillator_allocate0();

    [DllImport("__Internal")]
    public static extern void Oscillator_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Oscillator_perform0(IntPtr ptr, double f);

    [DllImport("__Internal")]
    public static extern void Oscillator_setPhase0(IntPtr ptr, double f);
#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr Oscillator_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void Oscillator_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Oscillator_perform0(IntPtr ptr, double f);

    [DllImport("pdplusplusUnity")]
    public static extern void Oscillator_setPhase0(IntPtr ptr, double f);
#endif

    private IntPtr m_Oscillator;

    public Oscillator()
    {
        this.m_Oscillator = Oscillator_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if(this.m_Oscillator != IntPtr.Zero)
        {
            Oscillator_free0(this.m_Oscillator);
            this.m_Oscillator = IntPtr.Zero;
        }

        if(mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    ~Oscillator()
    {
        Dispose(false);
    }

#region Wrapper Methods
    public double perform(double freq)
    {
        return Oscillator_perform0(this.m_Oscillator, freq);
    }

    public void setPhase(double ph)
    {
        Oscillator_setPhase0(this.m_Oscillator, ph);
    }

#endregion Wrapper Methods
}
