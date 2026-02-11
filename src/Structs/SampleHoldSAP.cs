using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct SampleHold
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr SampleHold_allocate0();

    [DllImport("__Internal")]
    public static extern void SampleHold_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double SampleHold_perform0(IntPtr ptr, double input, double control);

    [DllImport("__Internal")]
    public static extern void SampleHold_reset0(IntPtr ptr, double value);

    [DllImport("__Internal")]
    public static extern void SampleHold_set0(IntPtr ptr, double value);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr SampleHold_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void SampleHold_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double SampleHold_perform0(IntPtr ptr, double input, double control);

        [DllImport("pdplusplusUnity")]
        public static extern void SampleHold_reset0(IntPtr ptr, double value);

        [DllImport("pdplusplusUnity")]
        public static extern void SampleHold_set0(IntPtr ptr, double value);

#endif

        private IntPtr m_SampleHold;

        public void Create()
        {
            this.m_SampleHold = SampleHold_allocate0();
        }

        public void Dispose()
        {
            if (this.m_SampleHold != IntPtr.Zero)
            {
                SampleHold_free0(this.m_SampleHold);
                this.m_SampleHold = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform(double input, double control)
        {
            return SampleHold_perform0(this.m_SampleHold, input, control);
        }

        public void reset(double v)
        {
            SampleHold_reset0(this.m_SampleHold, v);
        }

        public void set(double v)
        {
            SampleHold_set0(this.m_SampleHold, v);
        }

        #endregion Wrapper Methods
    }

}