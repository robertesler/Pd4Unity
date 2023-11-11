using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Threshold : PdMaster, IDisposable
{

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Threshold_allocate0();

    [DllImport("__Internal")]
    public static extern void Threshold_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern int Threshold_perform0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern void Threshold_setValues0(IntPtr ptr, double ht, double hd, double lt, double ld);

    [DllImport("__Internal")]
    public static extern void sThreshold_setState0(IntPtr ptr, int s);

#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr Threshold_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void Threshold_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern int Threshold_perform0(IntPtr ptr, double input);

    [DllImport("pdplusplusUnity")]
    public static extern void Threshold_setValues0(IntPtr ptr, double ht, double hd, double lt, double ld);

    [DllImport("pdplusplusUnity")]
    public static extern void Threshold_setState0(IntPtr ptr, int s);

#endif

    private IntPtr m_Threshold;

    public Threshold()
    {
        this.m_Threshold = Threshold_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if (this.m_Threshold != IntPtr.Zero)
        {
            Threshold_free0(this.m_Threshold);
            this.m_Threshold = IntPtr.Zero;
        }

        if (mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    ~Threshold()
    {
        Dispose(false);
    }

    #region Wrapper Methods
    public int perform(double input)
    {
        return Threshold_perform0(this.m_Threshold, input);
    }

    public void setValues(double ht, double hd, double lt, double ld)
    {
        Threshold_setValues0(this.m_Threshold, ht, hd, lt, ld);
    }

    public void setState(int s)
    {
        Threshold_setState0(this.m_Threshold, s);
    }

        #endregion Wrapper Methods
    }
}

    

