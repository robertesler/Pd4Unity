using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class ComplexZeroReverse : IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr ComplexZeroReverse_allocate0();

    [DllImport("__Internal")]
    public static extern void ComplexZeroReverse_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double ComplexZeroReverse_perform0(IntPtr ptr, double in, double imag, double rcoef, double icoef);

    [DllImport("__Internal")]
    public static extern void ComplexZeroReverse_set0(IntPtr ptr, double Complex, double imaginary);

    [DllImport("__Internal")]
    public static extern void ComplexZeroReverse_clear0(IntPtr ptr);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr ComplexZeroReverse_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexZeroReverse_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double ComplexZeroReverse_perform0(IntPtr ptr, double input, double imag, double rcoef, double icoef);

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexZeroReverse_set0(IntPtr ptr, double Complex, double imaginary);

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexZeroReverse_clear0(IntPtr ptr);

#endif

        private IntPtr m_ComplexZeroReverse;

        public ComplexZeroReverse()
        {
            this.m_ComplexZeroReverse = ComplexZeroReverse_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_ComplexZeroReverse != IntPtr.Zero)
            {
                ComplexZeroReverse_free0(this.m_ComplexZeroReverse);
                this.m_ComplexZeroReverse = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~ComplexZeroReverse()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double input, double imag, double rcoef, double icoef)
        {
            return ComplexZeroReverse_perform0(this.m_ComplexZeroReverse, input, imag, rcoef, icoef); ;
        }

        public void set(double c, double i)
        {
            ComplexZeroReverse_set0(this.m_ComplexZeroReverse, c, i);
        }

        public void clear()
        {
            ComplexZeroReverse_clear0(this.m_ComplexZeroReverse);
        }

        #endregion Wrapper Methods
    }

}
