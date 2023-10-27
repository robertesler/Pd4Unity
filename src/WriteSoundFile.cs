using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;


namespace PdPlusPlus
{

    public class WriteSoundFile : PdMaster, IDisposable
    {

#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr WriteSoundFile_allocate0();

    [DllImport("__Internal")]
    public static extern void WriteSoundFile_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void WriteSoundFile_open0(IntPtr ptr, char[] f, int nChannels,
            long type, long format);

    [DllImport("__Internal")]
    public static extern void WriteSoundFile_start0(IntPtr ptr, double[] input);

    [DllImport("__Internal")]
    public static extern void WriteSoundFile_stop0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern void WriteSoundFile_print0(IntPtr ptr);
#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr WriteSoundFile_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void WriteSoundFile_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void WriteSoundFile_open0(IntPtr ptr, char[] f, int nChannels,
                long type, long format);

        [DllImport("pdplusplusUnity")]
        public static extern void WriteSoundFile_start0(IntPtr ptr, double[] input);

        [DllImport("pdplusplusUnity")]
        public static extern void WriteSoundFile_stop0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern void WriteSoundFile_print0(IntPtr ptr);

#endif

        private IntPtr m_WriteSoundFile;

        public WriteSoundFile()
        {
            this.m_WriteSoundFile = WriteSoundFile_allocate0();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_WriteSoundFile != IntPtr.Zero)
            {
                WriteSoundFile_free0(this.m_WriteSoundFile);
                this.m_WriteSoundFile = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~WriteSoundFile()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        //types
        //0 = FILE_RAW; /*!< STK RAW file type. */
        //1 = FILE_WAV; /*!< WAV file type. */
        //2 = FILE_SND; /*!< SND (AU) file type. */
        //3 = FILE_AIF; /*!< AIFF file type. */
        //4 = FILE_MAT; /*!< Matlab MAT-file type. */

        //formats
        //0 = STK_SINT8;   /*!< -128 to +127 */
        //1 = STK_SINT16;  /*!< -32768 to +32767 */
        //2 =STK_SINT24;  /*!< Lower 3 bytes of 32-bit signed integer. */
        //3 = STK_SINT32;  /*!< -2147483648 to +2147483647. */
        //4 = STK_FLOAT32; /*!< Normalized between plus/minus 1.0. */
        //5 = STK_FLOAT64; /*!< Normalized between plus/minus 1.0. */

        public void open(string file, int channels, long type, long format)
        {
            char[] f = file.ToCharArray();
            WriteSoundFile_open0(this.m_WriteSoundFile, f, channels, type, format);
        }

        public void start(double[] input)
        {
            WriteSoundFile_start0(this.m_WriteSoundFile, input);
        }

        public void stop()
        {
            WriteSoundFile_stop0(this.m_WriteSoundFile);
        }

        public void print()
        {
            WriteSoundFile_print0(this.m_WriteSoundFile);
        }

        #endregion Wrapper Methods
    }

}