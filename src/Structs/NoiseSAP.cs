using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct Noise
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Noise_allocate0(); 

    [DllImport("__Internal")]
    public static extern void Noise_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Noise_perform0(IntPtr ptr);
#else
        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Noise_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Noise_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Noise_perform0(IntPtr ptr);
#endif

        private IntPtr m_Noise;

        public void Create()
        {
            this.m_Noise = Noise_allocate0();
        }

        public void Dispose()
        {
            if (this.m_Noise != IntPtr.Zero)
            {
                Noise_free0(this.m_Noise);
                this.m_Noise = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform()
        {
            return Noise_perform0(this.m_Noise);
        }
        #endregion Wrapper Methods
    }
}