using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class SoundFiler : IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr SoundFiler_allocate0();

    [DllImport("__Internal")]
    public static extern void SoundFiler_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern double SoundFiler_read0(IntPtr ptr, string file);

    [DllImport("__Internal")]
    public static extern void SoundFiler_write0(IntPtr ptr, string fileName,
           unsigned int nChannels,
           long type,
           long format,
           IntPtr array,
           long count);

    [DllImport("__Internal")]
    public static extern IntPtr SoundFiler_getArray0(IntPtr);


#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr SoundFiler_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void SoundFiler_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double SoundFiler_read0(IntPtr ptr, string file);

        [DllImport("pdplusplusUnity")]
        public static extern void SoundFiler_write0(IntPtr ptr, string fileName,
               unsigned int nChannels,
               long type,
               long format,
               IntPtr array,
               long count);

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr SoundFiler_getArray0(IntPtr);

#endif

        private IntPtr m_SoundFiler;

        public SoundFiler()
        {
            this.m_SoundFiler = SoundFiler_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_SoundFiler != IntPtr.Zero)
            {
                SoundFiler_free0(this.m_SoundFiler);
                this.m_SoundFiler = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~SoundFiler()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public double read(string file)
        {
            return SoundFiler_read0(this.m_SoundFiler, file);
        }

        public void write(string fileName,
               int nChannels,
               long type,
               long format,
               IntPtr array,
               long count)
        {
            SoundFiler_write0(this.m_SoundFiler, fileName,
               nChannels,
               type,
               format,
               array,
               count);
        }


        public IntPtr getArray()
        {
            return SoundFiler_getArray0(this.m_SoundFiler);
        }

        #endregion Wrapper Methods
    }

}