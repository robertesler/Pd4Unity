using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class HighPass : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr HighPass_allocate0();

    [DllImport("__Internal")]
    public static extern void HighPass_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double HighPass_perform0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern void HighPass_setCutOff0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern void HighPass_clear0(IntPtr ptr, double q);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr HighPass_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void HighPass_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double HighPass_perform0(IntPtr ptr, double input);

        [DllImport("pdplusplusUnity")]
        public static extern void HighPass_setCutoff0(IntPtr ptr, double f);

        [DllImport("pdplusplusUnity")]
        public static extern void HighPass_clear0(IntPtr ptr, double q);
#endif

        private IntPtr m_HighPass;

        public HighPass()
        {
            this.m_HighPass = HighPass_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_HighPass != IntPtr.Zero)
            {
                HighPass_free0(this.m_HighPass);
                this.m_HighPass = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~HighPass()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double freq)
        {
            return HighPass_perform0(this.m_HighPass, freq);
        }

        public void setCutoff(double f)
        {
            HighPass_setCutoff0(this.m_HighPass, f);
        }

        public void clear(double q)
        {
            HighPass_clear0(this.m_HighPass, q);
        }

        #endregion Wrapper Methods
    }
}