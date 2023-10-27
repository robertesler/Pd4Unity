using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Convolution : IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Convolution_allocate0();

    [DllImport("__Internal")]
    public static extern void Convolution_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Convolution_perform0(IntPtr ptr, double filter, double control);

    [DllImport("__Internal")]
    public static extern void Convolution_setSquelch0(IntPtr ptr, int sq);

    [DllImport("__Internal")]
    public static extern int Convolution_getSquelch0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Convolution_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Convolution_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Convolution_perform0(IntPtr ptr, double filter, double control);

        [DllImport("pdplusplusUnity")]
        public static extern void Convolution_setSquelch0(IntPtr ptr, int sq);

        [DllImport("pdplusplusUnity")]
        public static extern int Convolution_getSquelch0(IntPtr ptr);

#endif

        private IntPtr m_Convolution;

        public Convolution()
        {
            this.m_Convolution = Convolution_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Convolution != IntPtr.Zero)
            {
                Convolution_free0(this.m_Convolution);
                this.m_Convolution = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Convolution()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double filter, double control)
        {
            return Convolution_perform0(this.m_Convolution, filter, control);
        }

        public void setSquelch(int sq)
        {
            Convolution_setSquelch0(this.m_Convolution, sq);
        }

        public int getSquelch()
        {
            return Convolution_getSquelch0(this.m_Convolution);
        }

        #endregion Wrapper Methods
    }

}