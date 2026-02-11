using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct Convolution
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Convolution_allocate0();

    [DllImport("__Internal")]
    public static extern void Convolution_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Convolution_perform0(IntPtr ptr, double filter, double control);

    [DllImport("__Internal")]
    public static extern void Convolution_setSquelch0(IntPtr ptr, int sq);

    [DllImport("__Internal")]
    public static extern int Convolution_getSquelch0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Convolution_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Convolution_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Convolution_perform0(IntPtr ptr, double filter, double control);

        [DllImport("pdplusplusUnity")]
        public static extern void Convolution_setSquelch0(IntPtr ptr, int sq);

        [DllImport("pdplusplusUnity")]
        public static extern int Convolution_getSquelch0(IntPtr ptr);

#endif

        private IntPtr m_Convolution;

        public void Create()
        {
            this.m_Convolution = Convolution_allocate0();
        }

        public void Dispose()
        {
            if (this.m_Convolution != IntPtr.Zero)
            {
                Convolution_free0(this.m_Convolution);
                this.m_Convolution = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform(double filter, double control)
        {
            return Convolution_perform0(this.m_Convolution, filter, control);
        }

        public void setSquelch(int sq)
        {
            Convolution_setSquelch0(this.m_Convolution, sq);
        }

        public int getSquelch()
        {
            return Convolution_getSquelch0(this.m_Convolution);
        }

        #endregion Wrapper Methods
    }

}