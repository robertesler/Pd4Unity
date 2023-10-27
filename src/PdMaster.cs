using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{


    public class PdMaster
    {
#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr PdMaster_allocate0();

    [DllImport("__Internal")]
    public static extern void PdMaster_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void PdMaster_setSampleRate0(IntPtr ptr, long sr);

    [DllImport("__Internal")]
    public static extern long PdMaster_getSampleRate0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void PdMaster_setBlockSize0(IntPtr ptr, int bs);

    [DllImport("__Internal")]
    public static extern int PdMaster_getBlockSize0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void PdMaster_setEndian0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern int PdMaster_getEndian0(IntPtr ptr);
    
        /*Converts samples into ms.*/
    [DllImport("__Internal")]
    public static extern double PdMaster_getTimeInSampleTicks0(IntPtr ptr);

        /*Converts milliseconds into samples/ms*/
    [DllImport("__Internal")]
    public static extern long PdMaster_getTimeInMilliSeconds0(IntPtr ptr, double time);
    
        /* A denormaling routine using C++'s numerics.*/
    [DllImport("__Internal")]
    public static extern int PdMaster_pdBigOrSmall0(IntPtr ptr, double f);
    
        /* Pure Data's denormaling routine*/
    [DllImport("__Internal")]
    public static extern int PdMaster_PD_BIGORSMALL0(IntPtr ptr, float f);

    [DllImport("__Internal")]
    public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);

    [DllImport("__Internal")]
    public static extern int PdMaster_getFFTWindow0(IntPtr ptr);
    
        /*acoustic conversions live here*/
    [DllImport("__Internal")]
    public static extern double PdMaster_mtof0(IntPtr ptr , double note); // MIDI note number to frequency

    [DllImport("__Internal")]
    public static extern double PdMaster_ftom0(IntPtr ptr , double freq); // Frequency to MIDI note number

    [DllImport("__Internal")]
    public static extern double PdMaster_powtodb0(IntPtr ptr, double num);

    [DllImport("__Internal")]
    public static extern double PdMaster_dbtopow0(IntPtr ptr, double db);

    [DllImport("__Internal")]
    public static extern double PdMaster_rmstodb0(IntPtr ptr, double num);  // RMS (e.g 0-1) to dB (0-100)

    [DllImport("__Internal")]
    public static extern double PdMaster_dbtorms0(IntPtr ptr, double db);
#else
    [DllImport("pdplusplusUnity")]
    public static extern IntPtr PdMaster_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void PdMaster_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void PdMaster_setSampleRate0(IntPtr ptr, long sr);

    [DllImport("pdplusplusUnity")]
    public static extern long PdMaster_getSampleRate0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void PdMaster_setBlockSize0(IntPtr ptr, int bs);

    [DllImport("pdplusplusUnity")]
    public static extern int PdMaster_getBlockSize0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void PdMaster_setEndian0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern int PdMaster_getEndian0(IntPtr ptr);

    /*Converts samples into ms.*/
    [DllImport("pdplusplusUnity")]
    public static extern double PdMaster_getTimeInSampleTicks0(IntPtr ptr);

    /*Converts milliseconds into samples/ms*/
    [DllImport("pdplusplusUnity")]
    public static extern long PdMaster_getTimeInMilliSeconds0(IntPtr ptr, double time);

    /* A denormaling routine using C++'s numerics.*/
    [DllImport("pdplusplusUnity")]
    public static extern int PdMaster_pdBigOrSmall0(IntPtr ptr, double f);

    /* Pure Data's denormaling routine*/
    [DllImport("pdplusplusUnity")]
    public static extern int PdMaster_PD_BIGORSMALL0(IntPtr ptr, float f);

    [DllImport("pdplusplusUnity")]
    public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);

    [DllImport("pdplusplusUnity")]
    public static extern int PdMaster_getFFTWindow0(IntPtr ptr);

    /*acoustic conversions live here*/
    [DllImport("pdplusplusUnity")]
    public static extern double PdMaster_mtof0(IntPtr ptr, double note); // MIDI note number to frequency

    [DllImport("pdplusplusUnity")]
    public static extern double PdMaster_ftom0(IntPtr ptr, double freq); // Frequency to MIDI note number

    [DllImport("pdplusplusUnity")]
    public static extern double PdMaster_powtodb0(IntPtr ptr, double num);

    [DllImport("pdplusplusUnity")]
    public static extern double PdMaster_dbtopow0(IntPtr ptr, double db);

    [DllImport("pdplusplusUnity")]
    public static extern double PdMaster_rmstodb0(IntPtr ptr, double num);  // RMS (e.g 0-1) to dB (0-100)

    [DllImport("pdplusplusUnity")]
    public static extern double PdMaster_dbtorms0(IntPtr ptr, double db);
#endif

        private IntPtr m_PdMaster;

        public PdMaster()
        {
            this.m_PdMaster = PdMaster_allocate0();
        }
         
        ~PdMaster()
        {
            if (this.m_PdMaster != IntPtr.Zero)
            {
                PdMaster_free0(this.m_PdMaster);
                this.m_PdMaster = IntPtr.Zero;
            }
            GC.SuppressFinalize(this);
        }

        #region Wrapper Methods

        public void setSampleRate(long sr)
        {
            PdMaster_setSampleRate0(this.m_PdMaster, sr);
        }

        public long getSampleRate()
        {
            return PdMaster_getSampleRate0(this.m_PdMaster); ;
        }
       
        public void setBlockSize(int bs)
        {
            PdMaster_setBlockSize0(this.m_PdMaster, bs);
        }

        public int getBlockSize()
        {
            return PdMaster_getBlockSize0(this.m_PdMaster);
        }

        /*This will auto-magically set the endian based on the system. */
        public void setEndian()
        {
            PdMaster_setEndian0(this.m_PdMaster);
        }

        public int getEndian()
        {
            return PdMaster_getEndian0(this.m_PdMaster);
        }

        public double getTimeInSampleTicks()
        {
            return PdMaster_getTimeInSampleTicks0(this.m_PdMaster);
        }

        public long getTimeInMilliSeconds(double time)
        {
            return PdMaster_getTimeInMilliSeconds0(this.m_PdMaster, time);
        }

        public int pdBigOrSmall(double f)
        {
            return PdMaster_pdBigOrSmall0(this.m_PdMaster, f);
        }

        public int PD_BIGORSMALL(float f)
        {
            return PdMaster_PD_BIGORSMALL0(this.m_PdMaster, f);
        }

        public void setFFTWindow(int w)
        {
            PdMaster_setFFTWindow0(this.m_PdMaster, w);
        }

        public int getFFTWindow()
        {
            return PdMaster_getFFTWindow0(this.m_PdMaster);
        }

        public double mtof(double note)
        {
            return PdMaster_mtof0(this.m_PdMaster, note);
        }

        public double ftom(double freq)
        {
            return PdMaster_ftom0(this.m_PdMaster, freq);
        }

        public double powtodb(double num)
        {
            return PdMaster_powtodb0(this.m_PdMaster, num);
        }

        public double dbtopow(double db)
        {
            return PdMaster_dbtopow0(this.m_PdMaster, db);
        }

        public double rmstodb(double num)
        {
            return PdMaster_rmstodb0(this.m_PdMaster, num);
        }

        public double dbtorms(double db)
        {
            return PdMaster_dbtorms0(this.m_PdMaster, db);
        }
        
        #endregion Wrapper Methods
    }
}
