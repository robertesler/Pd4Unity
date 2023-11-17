using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class TabRead4 : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr TabRead4_allocate0();

    [DllImport("__Internal")]
    public static extern void TabRead4_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double TabRead4_perform0(IntPtr ptr, double index);

    [DllImport("__Internal")]
    public static extern void TabRead4_setTable0(IntPtr ptr, [In] double[] table, long size);

    [DllImport("__Internal")]
    public static extern int TabRead4_getTableSize0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void TabRead4_setOnset0(IntPtr ptr, double);

    [DllImport("__Internal")]
    public static extern double TabRead4_getOnset0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr TabRead4_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void TabRead4_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double TabRead4_perform0(IntPtr ptr, double index);

        [DllImport("pdplusplusUnity")]
        public static extern void TabRead4_setTable0(IntPtr ptr, [In] double[] table, long size);

        [DllImport("pdplusplusUnity")]
        public static extern int TabRead4_getTableSize0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void TabRead4_setOnset0(IntPtr ptr, double o);

        [DllImport("pdplusplusUnity")]
        public static extern double TabRead4_getOnset0(IntPtr ptr);

#endif

        private IntPtr m_TabRead4;

        public TabRead4()
        {
            this.m_TabRead4 = TabRead4_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_TabRead4 != IntPtr.Zero)
            {
                TabRead4_free0(this.m_TabRead4);
                this.m_TabRead4 = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~TabRead4()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double index)
        {
            return TabRead4_perform0(this.m_TabRead4, index);
        }

        public void setTable(double[] table, long size)
        {
            TabRead4_setTable0(this.m_TabRead4, table, size);
        }

        public int getTableSize()
        {
            return TabRead4_getTableSize0(this.m_TabRead4);
        }

        public void setOnset(double onset)
        {
            TabRead4_setOnset0(this.m_TabRead4, onset);
        }

        public double getOnset()
        {
            return TabRead4_getOnset0(this.m_TabRead4);
        }

        #endregion Wrapper Methods
    }

}