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
    public static extern void ReadSoundFile_open0(IntPtr ptr, [In][MarshalAs(UnmanagedType.LPStr)] string file, double onset);

    [DllImport("__Internal")]
     public static extern void ReadSoundFile_start0(IntPtr ptr, [Out] double[] output);

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
    public static extern void ReadSoundFile_open0(IntPtr ptr, [In][MarshalAs(UnmanagedType.LPStr)] string file, double onset);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_start0(IntPtr ptr, [Out] double[] output);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_stop0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_print0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern void ReadSoundFile_setBufferSize0(IntPtr ptr, int bytes);

    [DllImport("pdplusplusUnity")]
    public static extern int ReadSoundFile_getBufferSize0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
     public static extern int ReadSoundFile_getChannels0(IntPtr ptr);

    [DllImport("pdplusplusUnity")]
    public static extern bool ReadSoundFile_isComplete0(IntPtr ptr);

#endif

        private IntPtr m_ReadSoundFile;
        private double[] output;
        private int buffer = 1024;

        public ReadSoundFile()
        {
            this.m_ReadSoundFile = ReadSoundFile_allocate0();
            output = new double[buffer];
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
        //call open() before start()
        public void open(string file, double onset)
        {
            ReadSoundFile_open0(this.m_ReadSoundFile, file, onset);
            buffer = getBufferSize();
            output = new double[buffer];
        }

        /*
         * This reads a file straight from disk.  For Unity this really isn't
         * all that necessary because Audio Source does this already. 
         * It's also not threaded, so it could fail if or glitch if the buffer
         * for some reason doesn't return in time or something else happens.
         * So I would say only use this if Audio Source or the Audio Clip
         * classes in the Unity API is not sufficient.
         * */
        public double[] start()
        {
            ReadSoundFile_start0(this.m_ReadSoundFile, output);
            return output;
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

        public int getChannels()
        {
            return ReadSoundFile_getChannels0(this.m_ReadSoundFile);
        }

        public bool isComplete()
        {
            return ReadSoundFile_isComplete0(this.m_ReadSoundFile);
        }

        #endregion Wrapper Methods
    }
}