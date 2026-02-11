using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct rFFT
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr rFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void rFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern int rFFT_perform0(IntPtr ptr, double input, [Out] double [] output);   
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr rFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void rFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int rFFT_perform0(IntPtr ptr, double input, [Out] double [] output);
#endif
        private IntPtr m_rFFT;
        private double[] buffer;
        private int winSize = 64;

        public void Create(int win)
        {
            this.m_rFFT = rFFT_allocate0();
            winSize = win;
            this.setFFTWindow(winSize);
            buffer = new double[winSize];
        }

        public void Create()
        {
            this.m_rFFT = rFFT_allocate0();
            this.setFFTWindow(winSize);//default 64
            buffer = new double[winSize];
        }

        public void Dispose()
        {
           if (this.m_rFFT != IntPtr.Zero)
            {
                rFFT_free0(this.m_rFFT);
                this.m_rFFT = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double[] perform(double input)
        {
            int f = rFFT_perform0(this.m_rFFT, input, buffer);
           // Debug.Log(buffer[0] + ", " + buffer[1] + ", " + buffer[2] + ", " + buffer[3]);
            return buffer;
        }


        #endregion Wrapper Methods
    }
}