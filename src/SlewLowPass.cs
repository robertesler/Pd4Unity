using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class SlewLowPass : IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr SlewLowPass_allocate0();

    [DllImport("__Internal")]
    public static extern void SlewLowPass_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double SlewLowPass_perform0(IntPtr ptr, double input, double freq, double posLimitIn, double posFreqIn, double negLimitIn, double negFreqIn );

    [DllImport("__Internal")]
    public static extern void SlewLowPass_set0(IntPtr ptr, double last);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr SlewLowPass_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void SlewLowPass_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double SlewLowPass_perform0(IntPtr ptr, double input, double freq, double posLimitIn, double posFreqIn, double negLimitIn, double negFreqIn);

        [DllImport("pdplusplusUnity")]
        public static extern void SlewLowPass_set0(IntPtr ptr, double last);

#endif

        private IntPtr m_SlewLowPass;

        public SlewLowPass()
        {
            this.m_SlewLowPass = SlewLowPass_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_SlewLowPass != IntPtr.Zero)
            {
                SlewLowPass_free0(this.m_SlewLowPass);
                this.m_SlewLowPass = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~SlewLowPass()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(IntPtr ptr, double input, double freq, double posLimitIn, double posFreqIn, double negLimitIn, double negFreqIn)
        {
            return SlewLowPass_perform0(this.m_SlewLowPass, input, freq, posLimitIn, posFreqIn, negLimitIn, negFreqIn);
        }

        public void set(double last)
        {
            SlewLowPass_set0(this.m_SlewLowPass, last);
        }

        #endregion Wrapper Methods
    }

}