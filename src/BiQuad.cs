using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class BiQuad : IDisposable
{

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr BiQuad_allocate0();

    [DllImport("__Internal")]
    public static extern void BiQuad_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double BiQuad_perform0(IntPtr ptr, double f);

    [DllImport("__Internal")]
    public static extern void BiQuad_setCoefficients0(IntPtr ptr, double fb1, double fb2, double ff1, double ff2, double ff3);

    DllImport("__Internal")]
    public static extern void BiQuad_set0(IntPtr ptr, double a, double b);
#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr BiQuad_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void BiQuad_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double BiQuad_perform0(IntPtr ptr, double f);

    [DllImport("pdplusplusUnity")]
    public static extern void BiQuad_setCoefficients0(IntPtr ptr, double fb1, double fb2, double ff1, double ff2, double ff3);

    [DllImport("pdplusplusUnity")]
    public static extern void BiQuad_set0(IntPtr ptr, double a, double b);
#endif

    private IntPtr m_BiQuad;

    public BiQuad()
    {
        this.m_BiQuad = BiQuad_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if (this.m_BiQuad != IntPtr.Zero)
        {
            BiQuad_free0(this.m_BiQuad);
            this.m_BiQuad = IntPtr.Zero;
        }

        if (mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    ~BiQuad()
    {
        Dispose(false);
    }

    #region Wrapper Methods
    public double perform(double input)
    {
        return BiQuad_perform0(this.m_BiQuad, input);
    }

    public void setCoefficients(double fb1, double fb2, double ff1, double ff2, double ff3)
    {
        BiQuad_setCoefficients0(this.m_BiQuad, fb1, fb2, ff1, ff2, ff3);
    }

    public void set(double a, double b)
    {
        BiQuad_set0(this.m_BiQuad, a, b);
    }

    #endregion Wrapper Methods
}
