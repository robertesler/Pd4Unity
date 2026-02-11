using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct rIFFT
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr rIFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void rIFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double rIFFT_perform0(IntPtr ptr, double* input;
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr rIFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void rIFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double rIFFT_perform0(IntPtr ptr, [In] double[] input);
#endif
        private IntPtr m_rIFFT;
        private int winSize = 64;

        public void Create(int win)
        {
            this.m_rIFFT = rIFFT_allocate0();
            winSize = win;
            this.setFFTWindow(winSize);
        }

        public void Create()
        {
            this.m_rIFFT = rIFFT_allocate0();
            this.setFFTWindow(winSize);//default 64
        }

        public void Dispose()
        {
           if (this.m_rIFFT != IntPtr.Zero)
            {
                rIFFT_free0(this.m_rIFFT);
                this.m_rIFFT = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform(double[] input)
        {
            double output = 0;
           
            output = rIFFT_perform0(this.m_rIFFT, input);
            return output;
        }


        #endregion Wrapper Methods
    }
}