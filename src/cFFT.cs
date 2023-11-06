using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class cFFT : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr cFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void cFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern int cFFT_perform0(IntPtr ptr, double real, double imaginary, [Out] double[] output);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr cFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void cFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int cFFT_perform0(IntPtr ptr, double real, double imaginary, [Out] double[] output);
#endif
        private IntPtr m_cFFT;
        private double[] output;
        private int winSize = 64;
        public cFFT(int win)
        {
            this.m_cFFT = cFFT_allocate0();
            winSize = win;
            this.setFFTWindow(winSize);
            output = new double[winSize*2];//complex FFT requires 2 * windowSize
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_cFFT != IntPtr.Zero)
            {
                cFFT_free0(this.m_cFFT);
                this.m_cFFT = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~cFFT()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double[] perform(double real, double imaginary)
        {
           int status = cFFT_perform0(this.m_cFFT, real, imaginary, output);
            if(status == 0)
            {
                Debug.Log("cFFT pointer invalid.");
            }
            return output;
        }
            

        #endregion Wrapper Methods
    }

}