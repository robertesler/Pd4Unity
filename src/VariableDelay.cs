using System.Runtime.InteropServices;
using System;


namespace PdPlusPlus
{

    public class VariableDelay : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr VariableDelay_allocate0();

    [DllImport("__Internal")]
    public static extern IntPtr VariableDelay_allocate1(double deltime);

    [DllImport("__Internal")]
    public static extern void VariableDelay_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void VariableDelay_delayWrite0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern double VariableDelay_delayRead0(IntPtr ptr, double delayTime);

    [DllImport("__Internal")]
    public static extern double VariableDelay_perform0(IntPtr ptr, double delayTime);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr VariableDelay_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr VariableDelay_allocate1(double deltime);

        [DllImport("pdplusplusUnity")]
        public static extern void VariableDelay_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void VariableDelay_delayWrite0(IntPtr ptr, double input);

        [DllImport("pdplusplusUnity")]
        public static extern double VariableDelay_delayRead0(IntPtr ptr, double delayTime);

        [DllImport("pdplusplusUnity")]
        public static extern double VariableDelay_perform0(IntPtr ptr, double delayTime);

#endif

        private IntPtr m_VariableDelay;

        public VariableDelay()
        {
            this.m_VariableDelay = VariableDelay_allocate0();
        }

        public VariableDelay(double deltime)
        {
            this.m_VariableDelay = VariableDelay_allocate1(deltime);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_VariableDelay != IntPtr.Zero)
            {
                VariableDelay_free0(this.m_VariableDelay);
                this.m_VariableDelay = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~VariableDelay()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double dt)
        {
            return VariableDelay_perform0(this.m_VariableDelay, dt);
        }
        public void delayWrite(double input)
        {
            VariableDelay_delayWrite0(this.m_VariableDelay, input);
        }

        public double delayRead(double dt)
        {
            return VariableDelay_delayRead0(this.m_VariableDelay, dt);
        }

        #endregion Wrapper Methods
    }
}

