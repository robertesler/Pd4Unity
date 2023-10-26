using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class Sigmund : IDisposable
{

    struct sigmundPackage
    {
        double pitch = 0;
        double notes = 0;
        double envelope = 0;
        double** peaks;
        double** tracks;
        int peakSize = 0;
        int trackSize = 0;

    };

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Sigmund_allocate0();

    [DllImport("__Internal")]
    public static extern void Sigmund_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern sigmundPackage Sigmund_perform0(IntPtr ptr, double input);

    [DllImport("__Internal")]
     public static extern void Sigmund_setMode0(IntPtr ptr, int mode);

     [DllImport("__Internal")]
     public static extern void Sigmund_setNumOfPoints0(IntPtr ptr, double n);

     [DllImport("__Internal")]
     public static extern void Sigmund_setHop0(IntPtr ptr, double h);

     [DllImport("__Internal")]
     public static extern void Sigmund_setNumOfPeaks0(IntPtr ptr, double p);

     [DllImport("__Internal")]
     public static extern void Sigmund_setMaxFrequency0(IntPtr ptr, double mf);

     [DllImport("__Internal")]
     public static extern void Sigmund_setVibrato0(IntPtr ptr, double v);

     [DllImport("__Internal")]
     public static extern void Sigmund_setStableTime0(IntPtr ptr, double st);

     [DllImport("__Internal")]
     public static extern void Sigmund_setMinPower0(IntPtr ptr, double mp);

     [DllImport("__Internal")]
     public static extern void Sigmund_setGrowth0(IntPtr ptr, double g);

     [DllImport("__Internal")]
     public static extern void Sigmund_print0(IntPtr ptr);//print all parameters

     [DllImport("__Internal")]
     public static extern sigmundPackage Sigmund_list0(IntPtr ptr, IntPtr array, int numOfPoints, int index, long sr, int debug);//read from an array

     [DllImport("__Internal")]
     public static extern void Sigmund_clear0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getMode0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getNumOfPoints0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getHop0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getNumOfPeaks0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getMaxFrequency0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getVibrato0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getStableTime0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getMinPower0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getGrowth0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getPeakColumnNumber0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getTrackColumnNumber0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getPeakBool0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getTrackBool0(IntPtr ptr);

#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr Sigmund_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern sigmundPackage Sigmund_perform0(IntPtr ptr, double input);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setMode0(IntPtr ptr, int mode);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setNumOfPoints0(IntPtr ptr, double n);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setHop0(IntPtr ptr, double h);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setNumOfPeaks0(IntPtr ptr, double p);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setMaxFrequency0(IntPtr ptr, double mf);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setVibrato0(IntPtr ptr, double v);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setStableTime0(IntPtr ptr, double st);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setMinPower0(IntPtr ptr, double mp);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_setGrowth0(IntPtr ptr, double g);

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_print0(IntPtr ptr);//print all parameters

    [DllImport("pdplusplusUnity")]
    public static extern sigmundPackage Sigmund_list0(IntPtr ptr, IntPtr array, int numOfPoints, int index, long sr, int debug);//read from an array

    [DllImport("pdplusplusUnity")]
    public static extern void Sigmund_clear0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern int Sigmund_getMode0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getNumOfPoints0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getHop0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getNumOfPeaks0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getMaxFrequency0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getVibrato0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getStableTime0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getMinPower0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern double Sigmund_getGrowth0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern int Sigmund_getPeakColumnNumber0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern int Sigmund_getTrackColumnNumber0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern int Sigmund_getPeakBool0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern int Sigmund_getTrackBool0(IntPtr ptr);

#endif

    private IntPtr m_Sigmund;

    public Sigmund()
    {
        this.m_Sigmund = Sigmund_allocate0();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool mDispose)
    {

        if (this.m_Sigmund != IntPtr.Zero)
        {
            Sigmund_free0(this.m_Sigmund);
            this.m_Sigmund = IntPtr.Zero;
        }

        if (mDispose)
        {
            GC.SuppressFinalize(this);
        }
    }

    ~Sigmund()
    {
        Dispose(false);
    }

    #region Wrapper Methods
    public sigmundPackage perform(double input)
    {
        return Sigmund_perform0(this.m_Sigmund, input);
    }

    public void setMode(int mode)
    {
        Sigmund_setMode0(this.m_Sigmund, mode);
    }

    public void setNumOfPoints(double n)
    {
        Sigmund_setNumOfPoints0(this.m_Sigmund, n);
    }

    public void setHop(double h)
    {
        Sigmund_setHop0(this.m_Sigmund, h);
    }

    public void setNumOfPeaks(double p)
    {
        Sigmund_setNumOfPeaks0(this.m_Sigmund, p);
    }

    public void setMaxFrequency(double mf)
    {
        Sigmund_setMaxFrequency0(this.m_Sigmund, mf);
    }

    public void setVibrato(double v)
    {
        Sigmund_setVibrato0(this.m_Sigmund, v);
    }

    public void setStableTime(double st)
    {
        Sigmund_setStableTime0(this.m_Sigmund, st);
    }

    public void setMinPower(double mp)
    {
        Sigmund_setMinPower0(this.m_Sigmund, mp);
    }

    public void setGrowth()
    {
        Sigmund_setGrowth0(this.m_Sigmund, g);
    }
  
    public void pring()
    {
        Sigmund_print0(this.m_Sigmund);//print all parameters
    }

    public sigmundPackage list(IntPtr array, int numOfPoints, int index, long sr, int debug)
    {
        return Sigmund_list0(this.m_Sigmund, array, numOfPoints, index, sr, debug);//read from an array
    }

    public void clear()
    {
        Sigmund_clear0(this.m_Sigmund);

    }

    public int getMode()
    {
        return Sigmund_getMode0(this.m_Sigmund);
    }

    public double getNumOfPoints()
    {
        return Sigmund_getNumOfPoints0(this.m_Sigmund);
    }

    public double getHop()
    {
        return Sigmund_getHop0(this.m_Sigmund);
    }

    public double getNumOfPeaks()
    {
        return Sigmund_getNumOfPeaks0(this.m_Sigmund);
    }

    public double getMaxFrequency()
    {
        return Sigmund_getMaxFrequency0(this.m_Sigmund);
    }

    public getVibrato()
    {
        return Sigmund_getVibrato0(this.m_Sigmund);
    }

    public double getStableTime()
    {
        return Sigmund_getStableTime0(this.m_Sigmund);
    }

    public double getMinPower()
    {
        return Sigmund_getMinPower0(this.m_Sigmund);
    }

    public double getGrowth()
    {
        return Sigmund_getGrowth0(this.m_Sigmund);
    }

    public int getPeakColumnNumber()
    {
        return Sigmund_getPeakColumnNumber0(this.m_Sigmund);
    }

    public int getTrackColumnNumber()
    {
        return Sigmund_getTrackColumnNumber0(this.m_Sigmund);
    }
    public int getPeakBool()
    {
        return Sigmund_getPeakBool0(this.m_Sigmund)
    }

    public int getTrackBool()
    {
        return Sigmund_getTrackBool0(this.m_Sigmund);
    }

    #endregion Wrapper Methods
}