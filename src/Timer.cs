using System.Runtime.InteropServices;
using System;


namespace PdPlusPlus
{

    public class Timer : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Timer_allocate0();

    [DllImport("__Internal")]
    public static extern void Timer_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Timer_perform0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void Timer_start0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void Timer_stop0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void Timer_reset0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Timer_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Timer_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Timer_perform0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void Timer_start0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void Timer_stop0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void Timer_reset0(IntPtr ptr);

#endif

        private IntPtr m_Timer;

        public Timer()
        {
            this.m_Timer = Timer_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Timer != IntPtr.Zero)
            {
                Timer_free0(this.m_Timer);
                this.m_Timer = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Timer()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform()
        {
            return Timer_perform0(this.m_Timer);
        }

        public void start()
        {
            Timer_start0(this.m_Timer);
        }

        public void stop()
        {
            Timer_stop0(this.m_Timer);
        }

        public void reset()
        {
            Timer_reset0(this.m_Timer);
        }


        #endregion Wrapper Methods
    }
}
