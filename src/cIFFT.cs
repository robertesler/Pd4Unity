using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class cIFFT : PdMaster, IDisposable
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
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr cIFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void cIFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern  cIFFT.complexFFTOutput cIFFT_perform0(IntPtr ptr, [In] double[] input  );

#endif
        private IntPtr m_cIFFT;
        private double[] output = new double[2];
        private int winSize = 64;
        private cIFFT.complexFFTOutput fft;
        public cIFFT(int win)
        {
            this.m_cIFFT = cIFFT_allocate0();
            winSize = win;
            this.setFFTWindow(winSize);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_cIFFT != IntPtr.Zero)
            {
                cIFFT_free0(this.m_cIFFT);
                this.m_cIFFT = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~cIFFT()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double[] perform(double[] input)
        {
            fft = cIFFT_perform0(this.m_cIFFT, input);
            output[0] = fft.real;
            output[1] = fft.imaginary;

            return output;
        }


        #endregion Wrapper Methods
    }

}