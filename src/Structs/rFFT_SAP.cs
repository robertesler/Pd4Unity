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

     [DllImport("__Internal")]
    public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr rFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void rFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int rFFT_perform0(IntPtr ptr, double input, [Out] double[] output);

        [DllImport("pdplusplusUnity")]
        public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);
#endif
        private IntPtr m_rFFT;
        private IntPtr pdMaster;
        private double[] buffer;
        private int winSize;

        /*
            We need pass a single reference to PdMasterSAP to this 
            object on creation so we have access to the FFT Window Size.
        */
        public void Create(int win, IntPtr pdMasterPtr)
        {
            this.m_rFFT = rFFT_allocate0();
            winSize = win;
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(winSize);
            buffer = new double[winSize];
        }

        public void Create(IntPtr pdMasterPtr)
        {
            this.m_rFFT = rFFT_allocate0();
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(64);//default 64
            buffer = new double[64];
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

        public void setFFTWindow(int w)
        {
            PdMaster_setFFTWindow0(this.pdMaster, w);
        }
        
        #endregion Wrapper Methods
    }
}