using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Delay : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Delay_allocate0();

    [DllImport("__Internal")]
    public static extern void Delay_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Delay_perform0(IntPtr ptr, double f);

    [DllImport("__Internal")]
    public static extern void Delay_setDelayTime0(IntPtr ptr, double time);

    [DllImport("__Internal")]
    public static extern void Delay_reset0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Delay_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Delay_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Delay_perform0(IntPtr ptr, double f);

        [DllImport("pdplusplusUnity")]
        public static extern void Delay_setDelayTime0(IntPtr ptr, double time);

        [DllImport("pdplusplusUnity")]
        public static extern void Delay_reset0(IntPtr ptr);

#endif

        private IntPtr m_Delay;

        public Delay()
        {
            this.m_Delay = Delay_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Delay != IntPtr.Zero)
            {
                Delay_free0(this.m_Delay);
                this.m_Delay = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Delay()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double input)
        {
            return Delay_perform0(this.m_Delay, input);
        }

        public void setDelayTime(double time)
        {
            Delay_setDelayTime0(this.m_Delay, time);
        }

        public void reset()
        {
            Delay_reset0(this.m_Delay);
        }

        #endregion Wrapper Methods
    }
}