using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class RealZero : IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr RealZero_allocate0();

    [DllImport("__Internal")]
    public static extern void RealZero_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double RealZero_perform0(IntPtr ptr, double r, double i);

    [DllImport("__Internal")]
    public static extern void RealZero_set0(IntPtr ptr, double real, double imaginary);

    [DllImport("__Internal")]
    public static extern void RealZero_clear0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr RealZero_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void RealZero_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double RealZero_perform0(IntPtr ptr, double r, double i);

        [DllImport("pdplusplusUnity")]
        public static extern void RealZero_set0(IntPtr ptr, double real, double imaginary);

        [DllImport("pdplusplusUnity")]
        public static extern void RealZero_clear0(IntPtr ptr);

#endif

        private IntPtr m_RealZero;

        public RealZero()
        {
            this.m_RealZero = RealZero_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_RealZero != IntPtr.Zero)
            {
                RealZero_free0(this.m_RealZero);
                this.m_RealZero = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~RealZero()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double r, double i)
        {
            return RealZero_perform0(this.m_RealZero, r, i);
        }

        public void set(double real, double imaginary)
        {
            RealZero_set0(this.m_RealZero, real, imaginary);
        }

        public void clear()
        {
            RealZero_clear0(this.m_RealZero);
        }

        #endregion Wrapper Methods
    }

}