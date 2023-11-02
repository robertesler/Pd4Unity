using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;


namespace PdPlusPlus
{

    public class VoltageControlFilter : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr VoltageControlFilter_allocate0();

    [DllImport("__Internal")]
    public static extern void VoltageControlFilter_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern IntPtr VoltageControlFilter_perform0(IntPtr ptr, double input, double centerFrequency);

    [DllImport("__Internal")]
    public static extern double VoltageControlFilter_perform1(IntPtr ptr, double input, double centerFrequency);

    [DllImport("__Internal")]
     public static extern double VoltageControlFilter_perform2(IntPtr ptr, double input, double centerFrequency);

    [DllImport("__Internal")]
	public static extern void VoltageControlFilter_setQ0(IntPtr ptr, double f);

    [DllImport("__Internal")]
    public static extern double VoltageControlFilter_getDouble(IntPtr ptr);
#else

        [DllImport("pdplusplusUnity")]
    public static extern IntPtr VoltageControlFilter_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void VoltageControlFilter_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr VoltageControlFilter_perform0(IntPtr ptr, double input, double centerFrequency);

    [DllImport("pdplusplusUnity")]
    public static extern double VoltageControlFilter_perform1(IntPtr ptr, double input, double centerFrequency);

    [DllImport("pdplusplusUnity")]
    public static extern double VoltageControlFilter_perform2(IntPtr ptr, double input, double centerFrequency);

    [DllImport("pdplusplusUnity")]
    public static extern void VoltageControlFilter_setQ0(IntPtr ptr, double f);

    [DllImport("pdplusplusUnity")]
    public static extern double VoltageControlFilter_getDouble(IntPtr ptr);

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
        public double[] perform(double input, double cf)
        {
            IntPtr ptr = VoltageControlFilter_perform0(this.m_VoltageControlFilter, input, cf);
            double[] output = new double[2];
            Marshal.Copy(ptr, output, 0, output.Length);
            return output;
        }

        //returns only the real value of the vcf, e.g the bandpass
        public double perform_real(double input, double cf)
        {
            double output = VoltageControlFilter_perform1(this.m_VoltageControlFilter, input, cf);
            return output;
        }

        //returns only the imaginary value of the vcf, e.g the lwopass
        public double perform_imag(double input, double cf)
        {
            double output = VoltageControlFilter_perform2(this.m_VoltageControlFilter, input, cf);
            return output;
        }

        public void setQ(double q)
        {
            VoltageControlFilter_setQ0(this.m_VoltageControlFilter, q);
        }

        public double getDouble()
        {
            return VoltageControlFilter_getDouble(this.m_VoltageControlFilter);
        }

        #endregion Wrapper Methods
    }
}
