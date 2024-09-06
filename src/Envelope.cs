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
    public static extern IntPtr Envelope_allocate1(int ws, int p);

    [DllImport("__Internal")]
    public static extern void Envelope_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static externdouble Envelope_perform0(IntPtr ptr, double input);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Envelope_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Envelope_allocate1(int ws, int p);

        [DllImport("pdplusplusUnity")]
        public static extern void Envelope_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Envelope_perform0(IntPtr ptr, double input);

#endif

        private IntPtr m_Envelope;

        public Envelope()
        {
            this.m_Envelope = Envelope_allocate0();
        }

        public Envelope(int ws, int p)
        {
            this.m_Envelope = Envelope_allocate1(ws, p);
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

        #endregion Wrapper Methods
    }

}