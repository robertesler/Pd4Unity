using System.Runtime.InteropServices;
using System;

namespace PdPlusPlusSAP
{

    public struct Line
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Line_allocate0();

    [DllImport("__Internal")]
    public static extern void Line_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Line_perform0(IntPtr ptr, double target, double time);

    [DllImport("__Internal")]
    public static extern void Line_stop0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void Line_set0(IntPtr ptr, double target, double time);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Line_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Line_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Line_perform0(IntPtr ptr, double target, double time);

        [DllImport("pdplusplusUnity")]
        public static extern void Line_stop0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void Line_set0(IntPtr ptr, double target, double time);
#endif

        private IntPtr m_Line;

        public void Create()
        {
            this.m_Line = Line_allocate0();
        }

        public void Dispose()
        {
           if (this.m_Line != IntPtr.Zero)
            {
                Line_free0(this.m_Line);
                this.m_Line = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform(double target, double time)
        {
            return Line_perform0(this.m_Line, target, time);
        }

        public void stop()
        {
            Line_stop0(this.m_Line);
        }

        public void set(double target, double time)
        {
            Line_set0(this.m_Line, target, time);
        }

        #endregion Wrapper Methods
    }
}