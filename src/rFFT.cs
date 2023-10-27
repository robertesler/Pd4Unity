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
    public static extern IntPtr rFFT_perform0(IntPtr ptr, double input);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr rFFT_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void rFFT_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr rFFT_perform0(IntPtr ptr, double input);
#endif
        private IntPtr m_rFFT;

        public rFFT()
        {
            this.m_rFFT = rFFT_allocate0();
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
        public IntPtr perform(double input)
        {
            return rFFT_perform0(this.m_rFFT, input);
        }


        #endregion Wrapper Methods
    }
}