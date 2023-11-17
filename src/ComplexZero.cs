using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class ComplexZero : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr ComplexZero_allocate0();

    [DllImport("__Internal")]
    public static extern void ComplexZero_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double ComplexZero_perform0(IntPtr ptr, double in, double imag, double rcoef, double icoef);

    [DllImport("__Internal")]
    public static extern void ComplexZero_set0(IntPtr ptr, double Complex, double imaginary);

    [DllImport("__Internal")]
    public static extern void ComplexZero_clear0(IntPtr ptr);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr ComplexZero_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexZero_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double ComplexZero_perform0(IntPtr ptr, double input, double imag, double rcoef, double icoef);

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexZero_set0(IntPtr ptr, double Complex, double imaginary);

        [DllImport("pdplusplusUnity")]
        public static extern void ComplexZero_clear0(IntPtr ptr);

#endif

        private IntPtr m_ComplexZero;

        public ComplexZero()
        {
            this.m_ComplexZero = ComplexZero_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_ComplexZero != IntPtr.Zero)
            {
                ComplexZero_free0(this.m_ComplexZero);
                this.m_ComplexZero = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~ComplexZero()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double input, double imag, double rcoef, double icoef)
        {
            return ComplexZero_perform0(this.m_ComplexZero, input, imag, rcoef, icoef); ;
        }

        public void set(double c, double i)
        {
            ComplexZero_set0(this.m_ComplexZero, c, i);
        }

        public void clear()
        {
            ComplexZero_clear0(this.m_ComplexZero);
        }

        #endregion Wrapper Methods
    }

}