using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class BandPass : IDisposable
    {
#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr BandPass_allocate0();

    [DllImport("__Internal")]
    public static extern void BandPass_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double BandPass_perform0(IntPtr ptr, double cf);

    [DllImport("__Internal")]
    public static extern void BandPass_setCenterFrequency0(IntPtr ptr, double cf);

    [DllImport("__Internal")]
    public static extern void BandPass_setQ0(IntPtr ptr, double q);

    [DllImport("__Internal")]
    public static extern void BandPass_clear0(IntPtr ptr);
#else
        [DllImport("pdplusplusUnity")]
        public static extern IntPtr BandPass_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void BandPass_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double BandPass_perform0(IntPtr ptr, double cf);

        [DllImport("pdplusplusUnity")]
        public static extern void BandPass_setCenterFrequency0(IntPtr ptr, double cf);

        [DllImport("pdplusplusUnity")]
        public static extern void BandPass_setQ0(IntPtr ptr, double q);

        [DllImport("pdplusplusUnity")]
        public static extern void BandPass_clear0(IntPtr ptr);
#endif

        private IntPtr m_BandPass;

        public BandPass()
        {
            this.m_BandPass = BandPass_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_BandPass != IntPtr.Zero)
            {
                BandPass_free0(this.m_BandPass);
                this.m_BandPass = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~BandPass()
        {
            Dispose(false);
        }

        #region Wrapper Methods

        public double perform(double cf)
        {
            return BandPass_perform0(this.m_BandPass, cf);
        }

        public void setCenterFrequency(double cf)
        {
            BandPass_setCenterFrequency0(this.m_BandPass, cf);
        }

        public void setQ(double cf)
        {
            BandPass_setQ0(this.m_BandPass, cf);
        }

        public void clear()
        {
            BandPass_clear0(this.m_BandPass);
        }

        #endregion Wrapper Methods

    }

}
