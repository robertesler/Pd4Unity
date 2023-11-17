using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Envelope : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Envelope_allocate0();

    [DllImport("__Internal")]
    public static extern void Envelope_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static externdouble Envelope_perform0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern void Envelope_setWindowSize0(IntPtr ptr, int ws);

    [DllImport("__Internal")]
    public static extern void Envelope_setPeriod0(IntPtr ptr, int p);

    [DllImport("__Internal")]
    public static extern int Envelope_getWindowSize0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern int Envelope_getPeriod0(IntPtr ptr);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Envelope_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Envelope_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Envelope_perform0(IntPtr ptr, double input);

    [DllImport("pdplusplusUnity")]
        public static extern void Envelope_setWindowSize0(IntPtr ptr, int ws);

        [DllImport("pdplusplusUnity")]
        public static extern void Envelope_setPeriod0(IntPtr ptr, int p);

        [DllImport("pdplusplusUnity")]
        public static extern int Envelope_getWindowSize0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int Envelope_getPeriod0(IntPtr ptr);
#endif

        private IntPtr m_Envelope;

        public Envelope()
        {
            this.m_Envelope = Envelope_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Envelope != IntPtr.Zero)
            {
                Envelope_free0(this.m_Envelope);
                this.m_Envelope = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Envelope()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double freq)
        {
            return Envelope_perform0(this.m_Envelope, freq);
        }

        public void setWindowSize(int ws)
        {
            Envelope_setWindowSize0(this.m_Envelope, ws);
        }

        public void setPeriod(int p)
        {
            Envelope_setPeriod0(this.m_Envelope, p);
        }

        public int getWindowSize()
        {
            return Envelope_getWindowSize0(this.m_Envelope);
        }

        public int getPeriod()
        {
            return Envelope_getPeriod0(this.m_Envelope);
        }

        #endregion Wrapper Methods
    }

}