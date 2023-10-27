using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;


namespace PdPlusPlus
{

    public class VoltageControlFilter : IDisposable
    {
        struct vcfOutput
        {
            double real;
            double imaginary;
        };

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr VoltageControlFilter_allocate0();

    [DllImport("__Internal")]
    public static extern void VoltageControlFilter_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern vcfOutput VoltageControlFilter_perform0(IntPtr ptr, double input, double centerFrequency);

    [DllImport("__Internal")]
	public static extern void VoltageControlFilter_setQ0(IntPtr ptr, double f);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr VoltageControlFilter_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void VoltageControlFilter_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern public static extern vcfOutput VoltageControlFilter_perform0(IntPtr ptr, double input, double centerFrequency);

        [DllImport("pdplusplusUnity")]
        public static extern void VoltageControlFilter_setQ0(IntPtr ptr, double f);

#endif

        private IntPtr m_VoltageControlFilter;

        public VoltageControlFilter()
        {
            this.m_VoltageControlFilter = VoltageControlFilter_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_VoltageControlFilter != IntPtr.Zero)
            {
                VoltageControlFilter_free0(this.m_VoltageControlFilter);
                this.m_VoltageControlFilter = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~VoltageControlFilter()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public vcfOutput perform(double input, double cf)
        {
            return VoltageControlFilter_perform0(this.m_VoltageControlFilter, input, dt);
        }

        public void setQ(double q)
        {
            VoltageControlFilter_setQ0(this.m_VoltageControlFilter, q);
        }


        #endregion Wrapper Methods
    }
}
