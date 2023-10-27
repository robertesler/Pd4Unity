using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class cIFFT : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr cIFFT_allocate0();

    [DllImport("__Internal")]
    public static extern void cIFFT_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern IntPtr cIFFT_perform0(IntPtr ptr, double* input;
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr cIFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void cIFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr cIFFT_perform0(IntPtr ptr, double[] input);
        
#endif
        private IntPtr m_cIFFT;

        public cIFFT()
        {
            this.m_cIFFT = cIFFT_allocate0();
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
        public IntPtr perform(double[] input)
        {
            return cIFFT_perform0(this.m_cIFFT, input);
        }


        #endregion Wrapper Methods
    }

}