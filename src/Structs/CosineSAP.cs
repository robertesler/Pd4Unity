using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct Cosine
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Cosine_allocate0();

    [DllImport("__Internal")]
    public static extern void Cosine_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Cosine_perform0(IntPtr ptr, double i);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Cosine_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Cosine_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Cosine_perform0(IntPtr ptr, double i);

#endif

        private IntPtr m_Cosine;

        public void Create()
        {
            this.m_Cosine = Cosine_allocate0();
        }

        public void Dispose()
        {
            if (this.m_Cosine != IntPtr.Zero)
            {
                Cosine_free0(this.m_Cosine);
                this.m_Cosine = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform(double input)
        {
            return Cosine_perform0(this.m_Cosine, input);
        }

        #endregion Wrapper Methods
    }
}