using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Cosine : PdMaster, IDisposable
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

        public Cosine()
        {
            this.m_Cosine = Cosine_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Cosine != IntPtr.Zero)
            {
                Cosine_free0(this.m_Cosine);
                this.m_Cosine = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Cosine()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double perform(double input)
        {
            return Cosine_perform0(this.m_Cosine, input);
        }



        #endregion Wrapper Methods
    }
}