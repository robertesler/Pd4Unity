using System.Runtime.InteropServices;
using System;


namespace PdPlusPlusSAP
{

    public struct Timer
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

        public void Create()
        {
            this.m_Timer = Timer_allocate0();
        }

        public void Dispose()
        {
            if (this.m_Timer != IntPtr.Zero)
            {
                Timer_free0(this.m_Timer);
                this.m_Timer = IntPtr.Zero;
            }
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
