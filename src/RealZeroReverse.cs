using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class RealZeroReverse : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr RealZeroReverse_allocate0();

    [DllImport("__Internal")]
    public static extern void RealZeroReverse_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double RealZeroReverse_perform0(IntPtr ptr, double r, double i);

    [DllImport("__Internal")]
    public static extern void RealZeroReverse_set0(IntPtr ptr, double real, double imaginary);

    [DllImport("__Internal")]
    public static extern void RealZeroReverse_clear0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr RealZeroReverse_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void RealZeroReverse_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double RealZeroReverse_perform0(IntPtr ptr, double r, double i);

        [DllImport("pdplusplusUnity")]
        public static extern void RealZeroReverse_set0(IntPtr ptr, double real, double imaginary);

        [DllImport("pdplusplusUnity")]
        public static extern void RealZeroReverse_clear0(IntPtr ptr);

#endif

        private IntPtr m_RealZeroReverse;

        public RealZeroReverse()
        {
            this.m_RealZeroReverse = RealZeroReverse_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_RealZeroReverse != IntPtr.Zero)
            {
                RealZeroReverse_free0(this.m_RealZeroReverse);
                this.m_RealZeroReverse = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~RealZeroReverse()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double r, double i)
        {
            return RealZeroReverse_perform0(this.m_RealZeroReverse, r, i);
        }

        public void set(double real, double imaginary)
        {
            RealZeroReverse_set0(this.m_RealZeroReverse, real, imaginary);
        }

        public void clear()
        {
            RealZeroReverse_clear0(this.m_RealZeroReverse);
        }

        #endregion Wrapper Methods
    }
}