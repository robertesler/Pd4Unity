using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{
    public class PhaseVocoder : PdMaster, IDisposable
    {
#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr PhaseVocoder_allocate0();

    [DllImport("__Internal")]
    public static extern void PhaseVocoder_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double PhaseVocoder_perform0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double PhaseVocoder_inSample0(IntPtr ptr, [In][MarshalAs(UnmanagedType.LPStr)] string file);

    [DllImport("__Internal")]
    public static extern void PhaseVocoder_setSpeed0(IntPtr ptr, double s);

    [DllImport("__Internal")]
    public static extern void PhaseVocoder_setTranspo0(IntPtr ptr, double t);

    [DllImport("__Internal")]
    public static extern void PhaseVocoder_setLock0(IntPtr ptr, int l);

    [DllImport("__Internal")]
    public static extern void PhaseVocoder_setRewind0(IntPtr ptr);

#else
        [DllImport("pdplusplusUnity")]
        public static extern IntPtr PhaseVocoder_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void PhaseVocoder_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double PhaseVocoder_perform0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double PhaseVocoder_inSample0(IntPtr ptr, [In][MarshalAs(UnmanagedType.LPStr)] string file);

        [DllImport("pdplusplusUnity")]
        public static extern void PhaseVocoder_setSpeed0(IntPtr ptr, double s);

        [DllImport("pdplusplusUnity")]
        public static extern void PhaseVocoder_setTranspo0(IntPtr ptr, double t);

        [DllImport("pdplusplusUnity")]
        public static extern void PhaseVocoder_setLock0(IntPtr ptr, int l);

        [DllImport("pdplusplusUnity")]
        public static extern void PhaseVocoder_setRewind0(IntPtr ptr);

#endif

        private IntPtr m_PhaseVocoder;

        public PhaseVocoder()
        {
            this.m_PhaseVocoder = PhaseVocoder_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_PhaseVocoder != IntPtr.Zero)
            {
                PhaseVocoder_free0(this.m_PhaseVocoder);
                this.m_PhaseVocoder = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~PhaseVocoder()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform()
        {
            return PhaseVocoder_perform0(this.m_PhaseVocoder);
        }

        public void inSample(string file)
        {
            PhaseVocoder_inSample0(this.m_PhaseVocoder, file);
        }

        public void setSpeed(double s)
        {
            PhaseVocoder_setSpeed0(this.m_PhaseVocoder, s);
        }

        public void setTranspo(double t)
        {
            PhaseVocoder_setTranspo0(this.m_PhaseVocoder, t);
        }

        public void setLock(int l)
        {
            PhaseVocoder_setLock0(this.m_PhaseVocoder, l);
        }

        public void setRewind()
        {
            PhaseVocoder_setRewind0(this.m_PhaseVocoder);
        }

        #endregion Wrapper Methods

    }
}//namespace