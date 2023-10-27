using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Phasor : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Phasor_allocate0();

    [DllImport("__Internal")]
    public static extern void Phasor_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double Phasor_perform0(IntPtr ptr, double input);

    [DllImport("__Internal")]
    public static extern void Phasor_setPhase0(IntPtr ptr, double ph);

    [DllImport("__Internal")]
    public static extern double Phasor_getPhase0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void Phasor_setFrequency0(IntPtr ptr, double f);

    [DllImport("__Internal")]
    public static extern double Phasor_getFrequency0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void Phasor_setVolume0(IntPtr ptr, double v);

    [DllImport("__Internal")]
    public static extern double Phasor_getVolume0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Phasor_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Phasor_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Phasor_perform0(IntPtr ptr, double input);

        [DllImport("pdplusplusUnity")]
        public static extern void Phasor_setPhase0(IntPtr ptr, double ph);

        [DllImport("pdplusplusUnity")]
        public static extern double Phasor_getPhase0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void Phasor_setFrequency0(IntPtr ptr, double f);

        [DllImport("pdplusplusUnity")]
        public static extern double Phasor_getFrequency0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void Phasor_setVolume0(IntPtr ptr, double v);

        [DllImport("pdplusplusUnity")]
        public static extern double Phasor_getVolume0(IntPtr ptr);

#endif

        private IntPtr m_Phasor;

        public Phasor()
        {
            this.m_Phasor = Phasor_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Phasor != IntPtr.Zero)
            {
                Phasor_free0(this.m_Phasor);
                this.m_Phasor = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Phasor()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double input)
        {
            return Phasor_perform0(this.m_Phasor, input);
        }

        public void setPhase(double pf)
        {
            Phasor_setPhase0(this.m_Phasor, pf);
        }

        public double getPhase()
        {
            return Phasor_getPhase0(this.m_Phasor);
        }

        public void setFrequency(double f)
        {
            Phasor_setFrequency0(this.m_Phasor, f);
        }

        public double getFrequency()
        {
            return Phasor_getFrequency0(this.m_Phasor);
        }

        public void setVolume(double v)
        {
            Phasor_setVolume0(this.m_Phasor, v);
        }

        public double getVolume()
        {
            return Phasor_getVolume0(this.m_Phasor);
        }

        #endregion Wrapper Methods
    }

}
