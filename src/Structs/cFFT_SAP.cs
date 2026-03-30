using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct cFFT
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr cFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void cFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern int cFFT_perform0(IntPtr ptr, double real, double imaginary, [Out] double[] output);

    [DllImport("__Internal")]
    public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr cFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void cFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int cFFT_perform0(IntPtr ptr, double real, double imaginary, [Out] double[] output);

        [DllImport("pdplusplusUnity")]
        public static extern void PdMaster_setFFTWindow0(IntPtr ptr, int w);
#endif
        private IntPtr m_cFFT;
        private IntPtr pdMaster;
        private double[] output;
        private int winSize;

        public void Create(int win, IntPtr pdMasterPtr)
        {
            this.m_cFFT = cFFT_allocate0();
            winSize = win;
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(winSize);
            output = new double[winSize * 2];//complex FFT requires 2 * windowSize
        }

        public void Create(IntPtr pdMasterPtr)
        {
            this.m_cFFT = cFFT_allocate0();
            this.pdMaster = pdMasterPtr;
            this.setFFTWindow(64);//default 64
            output = new double[64 * 2];//complex FFT requires 2 * windowSize   
        }

        public void Dispose()
        {
            if (this.m_cFFT != IntPtr.Zero)
            {
                cFFT_free0(this.m_cFFT);
                this.m_cFFT = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double[] perform(double real, double imaginary)
        {
            int status = cFFT_perform0(this.m_cFFT, real, imaginary, output);
            if (status == 0)
            {
                Console.Write("cFFT pointer invalid.");
            }
            return output;
        }
        
        public void setFFTWindow(int win)
        {
            PdMaster_setFFTWindow0(this.pdMaster, win);
        }

        #endregion Wrapper Methods
    }

}