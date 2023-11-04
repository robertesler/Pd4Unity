using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class rFFT : PdMaster, IDisposable
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
        public rFFT(int win)
        {
            this.m_rFFT = rFFT_allocate0();
            winSize = win;
            this.setFFTWindow(winSize);
            buffer = new double[winSize];
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_rFFT != IntPtr.Zero)
            {
                rFFT_free0(this.m_rFFT);
                this.m_rFFT = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~rFFT()
        {
            Dispose(false);
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