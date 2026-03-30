using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct cIFFT
    {
        public struct complexFFTOutput {
            public double real;
            public double imaginary;
        }

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr cIFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void cIFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern int cIFFT_perform0(IntPtr ptr, [In] double[] input, [Out] double[]);

    [DllImport("__Internal")]
    public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr cIFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void cIFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern cIFFT.complexFFTOutput cIFFT_perform0(IntPtr ptr, [In] double[] input);

        [DllImport("pdplusplusUnity")]
        public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);


#endif
        private IntPtr m_cIFFT;
        private IntPtr pdMaster;
        private double[] output;
        private int winSize;
        private cIFFT.complexFFTOutput fft;

        /*
        We have to make sure to create one version of PdMaster before we create any cIFFT instances, 
        because cIFFT relies on PdMaster for its window size. 
        So we pass in a pointer to the PdMaster instance when we create the cIFFT instance.

        You will then reuse the pdMaster IntPtr in all other objects as well.
        See examples for how this will work.
        */
        public cIFFT(int ws, IntPtr pdMasterPtr)
        {
            this.m_cIFFT = IntPtr.Zero;
            fft = new cIFFT.complexFFTOutput();
            output = new double[2];
            winSize = ws;
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(winSize);
        }

        public void Create(int win, IntPtr pdMasterPtr)
        {
            this.m_cIFFT = cIFFT_allocate0();
            fft = new cIFFT.complexFFTOutput();
            output = new double[2];
            winSize = win;
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(winSize);
        }

        public void Create(IntPtr pdMasterPtr)
        {
            fft = new cIFFT.complexFFTOutput();
            output = new double[2];
            winSize = 64;//default
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(winSize);
        }

        public void Dispose()
        {
            if (this.m_cIFFT != IntPtr.Zero)
            {
                cIFFT_free0(this.m_cIFFT);
                this.m_cIFFT = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double[] perform(double[] input)
        {
            fft = cIFFT_perform0(this.m_cIFFT, input);
            output[0] = fft.real;
            output[1] = fft.imaginary;

            return output;
        }

        public void setFFTWindow(int w)
        {
            PdMaster_setFFTWindow0(this.pdMaster, w);
        }

        #endregion Wrapper Methods
    }

}