using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class ComplexPole : IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr ComplexPole_allocate0();

    [DllImport("__Internal")]
    public static extern void ComplexPole_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double ComplexPole_perform0(IntPtr ptr, double in, double imag, double rcoef, double icoef);

    [DllImport("__Internal")]
    public static extern void ComplexPole_set0(IntPtr ptr, double Complex, double imaginary);

    [DllImport("__Internal")]
    public static extern void ComplexPole_clear0(IntPtr ptr);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr ComplexPole_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexPole_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double ComplexPole_perform0(IntPtr ptr, double input, double imag, double rcoef, double icoef);

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexPole_set0(IntPtr ptr, double Complex, double imaginary);

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexPole_clear0(IntPtr ptr);

#endif

        private IntPtr m_ComplexPole;

        public ComplexPole()
        {
            this.m_ComplexPole = ComplexPole_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_ComplexPole != IntPtr.Zero)
            {
                ComplexPole_free0(this.m_ComplexPole);
                this.m_ComplexPole = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~ComplexPole()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double input, double imag, double rcoef, double icoef)
        {
            return ComplexPole_perform0(this.m_ComplexPole, input, imag, rcoef, icoef); ;
        }

        public void set(double c, double i)
        {
            ComplexPole_set0(this.m_ComplexPole, c, i);
        }

        public void clear()
        {
            ComplexPole_clear0(this.m_ComplexPole);
        }

        #endregion Wrapper Methods
    }

}
