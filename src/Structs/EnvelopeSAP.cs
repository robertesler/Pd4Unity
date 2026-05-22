using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct Envelope
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

        public void Create()
        {
            this.m_Envelope = Envelope_allocate0();
        }

        public void Create(int ws, int p)
        {
            this.m_Envelope = Envelope_allocate1(ws, p);
        }

        public void Dispose()
        {
            if (this.m_Envelope != IntPtr.Zero)
            {
                Envelope_free0(this.m_Envelope);
                this.m_Envelope = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform(double freq)
        {
            return Envelope_perform0(this.m_Envelope, freq);
        }

        #endregion Wrapper Methods
    }

}