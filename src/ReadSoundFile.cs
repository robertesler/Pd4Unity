using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class ReadSoundFile : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr ReadSoundFile_allocate0();

    [DllImport("__Internal")]
    public static extern void ReadSoundFile_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void ReadSoundFile_open0(IntPtr ptr, char[] file, double onset);

    [DllImport("__Internal")]
     public static extern IntPtr ReadSoundFile_start0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void ReadSoundFile_stop0(IntPtr ptr);

    [DllImport("__Internal")]
     public static extern void ReadSoundFile_print0(IntPtr ptr);

    [DllImport("__Internal")]
     public static extern void ReadSoundFile_setBufferSize0(IntPtr ptr, int bytes);

    [DllImport("__Internal")]
     public static extern int ReadSoundFile_getBufferSize0(IntPtr ptr);

    [DllImport("__Internal")]
     public static extern bool ReadSoundFile_isComplete0(IntPtr ptr);

#else

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr ReadSoundFile_allocate0();

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_free0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_open0(IntPtr ptr, char[] file, double onset);

    [DllImport("pdplusplusUnity")]
    public static extern IntPtr ReadSoundFile_start0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_stop0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_print0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_setBufferSize0(IntPtr ptr, int bytes);

    [DllImport("pdplusplusUnity")]
    public static extern int ReadSoundFile_getBufferSize0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern bool ReadSoundFile_isComplete0(IntPtr ptr);

#endif

        private IntPtr m_ReadSoundFile;

        public ReadSoundFile()
        {
            this.m_ReadSoundFile = ReadSoundFile_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_ReadSoundFile != IntPtr.Zero)
            {
                ReadSoundFile_free0(this.m_ReadSoundFile);
                this.m_ReadSoundFile = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~ReadSoundFile()
        {
            Dispose(false);
        }

        #region Wrapper Methods

        public void open(string file, double onset)
        {
            char[] f = file.ToCharArray();
            ReadSoundFile_open0(this.m_ReadSoundFile, f, onset);
        }

        public IntPtr start()
        {
            return ReadSoundFile_start0(this.m_ReadSoundFile);
        }

        public void stop()
        {
            ReadSoundFile_stop0(this.m_ReadSoundFile);
        }

        public void print()
        {
            ReadSoundFile_print0(this.m_ReadSoundFile);
        }

        public void setBufferSize(int bytes)
        {
            ReadSoundFile_setBufferSize0(this.m_ReadSoundFile, bytes);
        }

        public int getBufferSize()
        {
            return ReadSoundFile_getBufferSize0(this.m_ReadSoundFile);
        }

        public bool isComplete()
        {
            return ReadSoundFile_isComplete0(this.m_ReadSoundFile);
        }

        #endregion Wrapper Methods
    }
}