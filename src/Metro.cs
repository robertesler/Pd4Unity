using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Metro : IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Metro_allocate0();

    [DllImport("__Internal")]
    public static extern void Metro_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Metro_perform0(IntPtr ptr, double time);

    [DllImport("__Internal")]
    public static extern void Metro_setBPM0(IntPtr ptr, bool b);

    [DllImport("__Internal")]
    public static extern bool Metro_getBPM0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Metro_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Metro_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Metro_perform0(IntPtr ptr, double time);

        [DllImport("pdplusplusUnity")]
        public static extern void Metro_setBPM0(IntPtr ptr, bool b);

        [DllImport("pdplusplusUnity")]
        public static extern bool Metro_getBPM0(IntPtr ptr);

#endif

        private IntPtr m_Metro;

        public Metro()
        {
            this.m_Metro = Metro_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Metro != IntPtr.Zero)
            {
                Metro_free0(this.m_Metro);
                this.m_Metro = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Metro()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double input)
        {
            return Metro_perform0(this.m_Metro, input);
        }

        public void setBPM(bool t)
        {
            Metro_setBPM0(this.m_Metro, t);
        }

        public bool getBPM()
        {
            return Metro_getBPM0(this.m_Metro);
        }

        #endregion Wrapper Methods
    }
}