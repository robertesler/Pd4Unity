using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class BobFilter : PdMaster, IDisposable
    {
#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr BobFilter_allocate0();

    [DllImport("__Internal")]
    public static extern void BobFilter_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double BobFilter_perform0(IntPtr ptr, double f);

    [DllImport("__Internal")]
    public static extern void BobFilter_setCutoffFrequency0(IntPtr ptr, double cf);

    [DllImport("__Internal")]
    public static extern void BobFilter_setResonance0(IntPtr ptr, double r);

    [DllImport("__Internal")]
    public static extern void BobFilter_setSaturation0(IntPtr ptr, double s);

    [DllImport("__Internal")]
    public static extern void BobFilter_setOversampling0(IntPtr ptr, double o);

    [DllImport("__Internal")]
    public static extern double BobFilter_getCutoffFrequency0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double BobFilter_getResonance0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void BobFilter_error0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void BobFilter_clear0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void BobFilter_print0(IntPtr ptr);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr BobFilter_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double BobFilter_perform0(IntPtr ptr, double f);

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_setCutoffFrequency0(IntPtr ptr, double cf);

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_setResonance0(IntPtr ptr, double r);

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_setSaturation0(IntPtr ptr, double s);

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_setOversampling0(IntPtr ptr, double o);

        [DllImport("pdplusplusUnity")]
        public static extern double BobFilter_getCutoffFrequency0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double BobFilter_getResonance0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_error0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_clear0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void BobFilter_print0(IntPtr ptr);
#endif

        private IntPtr m_BobFilter;

        public BobFilter()
        {
            this.m_BobFilter = BobFilter_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_BobFilter != IntPtr.Zero)
            {
                BobFilter_free0(this.m_BobFilter);
                this.m_BobFilter = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~BobFilter()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double freq)
        {
            return BobFilter_perform0(this.m_BobFilter, freq);
        }

        public void setCutoffFrequency(double cf)
        {
            BobFilter_setCutoffFrequency0(this.m_BobFilter, cf);
        }

        public void setResonance(double r)
        {
            BobFilter_setResonance0(this.m_BobFilter, r);
        }

        public void setSaturation(double s)
        {
            BobFilter_setSaturation0(this.m_BobFilter, s);
        }

        public void setOversampling(double o)
        {
            BobFilter_setOversampling0(this.m_BobFilter, o);
        }

        public double getCutoffFrequency()
        {
            return BobFilter_getCutoffFrequency0(this.m_BobFilter);
        }

        public double getResonance()
        {
            return BobFilter_getResonance0(this.m_BobFilter);
        }

        public void error()
        {
            BobFilter_error0(this.m_BobFilter);
        }

        public void clear()
        {
            BobFilter_clear0(this.m_BobFilter);
        }

        public void print()
        {
            BobFilter_print0(this.m_BobFilter);
        }

        #endregion Wrapper Methods
    }

}