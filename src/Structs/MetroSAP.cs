using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct Metro
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Metro_allocate0();

    [DllImport("__Internal")]
    public static extern void Metro_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern bool Metro_perform0(IntPtr ptr, double time);

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
        public static extern bool Metro_perform0(IntPtr ptr, double time);

        [DllImport("pdplusplusUnity")]
        public static extern void Metro_setBPM0(IntPtr ptr, bool b);

        [DllImport("pdplusplusUnity")]
        public static extern bool Metro_getBPM0(IntPtr ptr);

#endif

        private IntPtr m_Metro;

        public void Create()
        {
            this.m_Metro = Metro_allocate0();
        }

        public void Dispose()
        {
            if (this.m_Metro != IntPtr.Zero)
            {
                Metro_free0(this.m_Metro);
                this.m_Metro = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public bool perform(double input)
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