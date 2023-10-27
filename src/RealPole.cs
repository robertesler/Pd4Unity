using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class RealPole : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr RealPole_allocate0();

    [DllImport("__Internal")]
    public static extern void RealPole_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double RealPole_perform0(IntPtr ptr, double r, double i);

    [DllImport("__Internal")]
    public static extern void RealPole_set0(IntPtr ptr, double real, double imaginary);

    [DllImport("__Internal")]
    public static extern void RealPole_clear0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr RealPole_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void RealPole_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double RealPole_perform0(IntPtr ptr, double r, double i);

        [DllImport("pdplusplusUnity")]
        public static extern void RealPole_set0(IntPtr ptr, double real, double imaginary);

        [DllImport("pdplusplusUnity")]
        public static extern void RealPole_clear0(IntPtr ptr);

#endif

        private IntPtr m_RealPole;

        public RealPole()
        {
            this.m_RealPole = RealPole_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_RealPole != IntPtr.Zero)
            {
                RealPole_free0(this.m_RealPole);
                this.m_RealPole = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~RealPole()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double r, double i)
        {
            return RealPole_perform0(this.m_RealPole, r, i);
        }

        public void set(double real, double imaginary)
        {
            RealPole_set0(this.m_RealPole, real, imaginary);
        }

        public void clear()
        {
            RealPole_clear0(this.m_RealPole);
        }

        #endregion Wrapper Methods
    }
}