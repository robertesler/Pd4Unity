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
    
    [DllImport("__Internal")]
    public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr rIFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void rIFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double rIFFT_perform0(IntPtr ptr, [In] double[] input);
        [DllImport("pdplusplusUnity")]
        public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);
#endif
        private IntPtr m_rIFFT;
        private IntPtr pdMaster;
        private int winSize;

        /*
            We need pass a single reference to PdMasterSAP to this 
            object on creation so we have access to the FFT Window Size.
        */

        public void Create(int win, IntPtr pdMasterPtr)
        {
            this.m_rIFFT = rIFFT_allocate0();
            winSize = win;
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(winSize);
        }

        public void Create(IntPtr pdMasterPtr)
        {
            this.m_rIFFT = rIFFT_allocate0();
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(64);//default 64
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

        public void setFFTWindow(int w)
        {
            PdMaster_setFFTWindow0(this.pdMaster, w);
        }

        #endregion Wrapper Methods
    }
}