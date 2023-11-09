using System.Runtime.InteropServices;
using System;

namespace PdPlusPlus
{

    public class Sigmund : PdMaster, IDisposable
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct sigmundPackage
        {
            public double pitch;
            public double notes;
            public double envelope;
            public int peakSize;
            public int trackSize;

        };


#if UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern IntPtr Sigmund_allocate0();

    [DllImport("__Internal")]
    public static extern IntPtr Sigmund_allocate0([In][MarshalAs(UnmanagedType.LPStr)] string peaks, [In][MarshalAs(UnmanagedType.LPStr)] string env);

    [DllImport("__Internal")]
    public static extern IntPtr Sigmund_allocate0([In][MarshalAs(UnmanagedType.LPStr)] string peaks, int num);

    [DllImport("__Internal")]
    public static extern void Sigmund_free0(IntPtr ptr);

    [DllImport("__Internal")]
    public static extern Sigmund.sigmundPackage Sigmund_perform0(IntPtr ptr, bool peaks, double input, [Out] double[] output);

    [DllImport("__Internal")]
     public static extern void Sigmund_setMode0(IntPtr ptr, int mode);

     [DllImport("__Internal")]
     public static extern void Sigmund_setNumOfPoints0(IntPtr ptr, double n);

     [DllImport("__Internal")]
     public static extern void Sigmund_setHop0(IntPtr ptr, double h);

     [DllImport("__Internal")]
     public static extern void Sigmund_setNumOfPeaks0(IntPtr ptr, double p);

     [DllImport("__Internal")]
     public static extern void Sigmund_setMaxFrequency0(IntPtr ptr, double mf);

     [DllImport("__Internal")]
     public static extern void Sigmund_setVibrato0(IntPtr ptr, double v);

     [DllImport("__Internal")]
     public static extern void Sigmund_setStableTime0(IntPtr ptr, double st);

     [DllImport("__Internal")]
     public static extern void Sigmund_setMinPower0(IntPtr ptr, double mp);

     [DllImport("__Internal")]
     public static extern void Sigmund_setGrowth0(IntPtr ptr, double g);

     [DllImport("__Internal")]
     public static extern void Sigmund_print0(IntPtr ptr);//print all parameters

     [DllImport("__Internal")]
     public static extern Sigmund.sigmundPackage Sigmund_list0(IntPtr ptr, bool peaks, [In] double[] array, int numOfPoints, int index, long sr, int debug, [Out] double[] output);//read from an array

     [DllImport("__Internal")]
     public static extern void Sigmund_clear0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getMode0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getNumOfPoints0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getHop0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getNumOfPeaks0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getMaxFrequency0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getVibrato0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getStableTime0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getMinPower0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern double Sigmund_getGrowth0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getPeakColumnNumber0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getTrackColumnNumber0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getPeakBool0(IntPtr ptr);

     [DllImport("__Internal")]
     public static extern int Sigmund_getTrackBool0(IntPtr ptr);

#else

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Sigmund_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Sigmund_allocate_pitch0([In][MarshalAs(UnmanagedType.LPStr)] string peaks, [In][MarshalAs(UnmanagedType.LPStr)] string env);

        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Sigmund_allocate_tracks0([In][MarshalAs(UnmanagedType.LPStr)] string peaks, int num);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern Sigmund.sigmundPackage Sigmund_perform0(IntPtr ptr, bool peaks, double input, [Out] double[] output);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setMode0(IntPtr ptr, int mode);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setNumOfPoints0(IntPtr ptr, double n);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setHop0(IntPtr ptr, double h);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setNumOfPeaks0(IntPtr ptr, double p);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setMaxFrequency0(IntPtr ptr, double mf);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setVibrato0(IntPtr ptr, double v);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setStableTime0(IntPtr ptr, double st);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setMinPower0(IntPtr ptr, double mp);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_setGrowth0(IntPtr ptr, double g);

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_print0(IntPtr ptr);//print all parameters

        [DllImport("pdplusplusUnity")]
        public static extern Sigmund.sigmundPackage Sigmund_list0(IntPtr ptr, bool peaks, [In] double[] array, int numOfPoints, int index, long sr, int debug, [Out] double[] output);//read from an array

        [DllImport("pdplusplusUnity")]
        public static extern void Sigmund_clear0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int Sigmund_getMode0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getNumOfPoints0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getHop0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getNumOfPeaks0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getMaxFrequency0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getVibrato0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getStableTime0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getMinPower0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Sigmund_getGrowth0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int Sigmund_getPeakColumnNumber0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int Sigmund_getTrackColumnNumber0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int Sigmund_getPeakBool0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern int Sigmund_getTrackBool0(IntPtr ptr);

#endif

        private IntPtr m_Sigmund;
        private bool peaks;
        private int numOfPeaks;
        private int numOfTracks;
        private double[] buffer;
        public double pitch {get; set;}
        public double envelope {get; set;}
        public double notes { get; set; }
        public int peakSize { get; set; }
        public int trackSize { get; set; }
        public double[] output { get; set; }
        public int sizeRow { get; set; }
        public int sizeColumn { get; set; }

        public Sigmund()
        {
            this.m_Sigmund = Sigmund_allocate0();
            peaks = false;
          
           
        }

        //choose either "pitch" or "notes", and "env" or "envelope"
        public Sigmund(string pitch, string env)
        {
            this.m_Sigmund = Sigmund_allocate_pitch0(pitch, env);
            peaks = false;
           
        }

        //choose either "peaks" or "tracks"
        public Sigmund(string tracks, int num)
        {
            this.m_Sigmund = Sigmund_allocate_tracks0(tracks, num);
            peaks = true;
            bool comp = tracks.Equals("peaks");
            if(comp)
            {
                buffer = new double[num * 5];//peaks are 5 columns, tracks are 4
            }
            else
            {
                buffer = new double[num * 4];//peaks are 5 columns, tracks are 4
            }
           
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool mDispose)
        {

            if (this.m_Sigmund != IntPtr.Zero)
            {
                Sigmund_free0(this.m_Sigmund);
                this.m_Sigmund = IntPtr.Zero;
            }

            if (mDispose)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~Sigmund()
        {
            Dispose(false);
        }

        #region Wrapper Methods
        public void perform(double input)
        {
            Sigmund.sigmundPackage aPack;

            aPack = Sigmund_perform0(this.m_Sigmund, peaks, input, buffer);
            envelope = aPack.envelope;
            notes = aPack.notes;
            pitch = aPack.pitch;
            peakSize = aPack.peakSize;
            trackSize = aPack.trackSize;
            sizeRow = buffer.Length;
            output = buffer;

        }

        public void setMode(int mode)
        {
            Sigmund_setMode0(this.m_Sigmund, mode);
        }

        public void setNumOfPoints(double n)
        {
            Sigmund_setNumOfPoints0(this.m_Sigmund, n);
        }

        public void setHop(double h)
        {
            Sigmund_setHop0(this.m_Sigmund, h);
        }

        public void setNumOfPeaks(double p)
        {
            Sigmund_setNumOfPeaks0(this.m_Sigmund, p);
        }

        public void setMaxFrequency(double mf)
        {
            Sigmund_setMaxFrequency0(this.m_Sigmund, mf);
        }

        public void setVibrato(double v)
        {
            Sigmund_setVibrato0(this.m_Sigmund, v);
        }

        public void setStableTime(double st)
        {
            Sigmund_setStableTime0(this.m_Sigmund, st);
        }

        public void setMinPower(double mp)
        {
            Sigmund_setMinPower0(this.m_Sigmund, mp);
        }

        public void setGrowth(double g)
        {
            Sigmund_setGrowth0(this.m_Sigmund, g);
        }

        public void pring()
        {
            Sigmund_print0(this.m_Sigmund);//print all parameters
        }

        public void list(double[] array, int numOfPoints, int index, long sr, int debug)
        {
            sigmundPackage aPack;
           
            aPack = Sigmund_list0(this.m_Sigmund, peaks, array, numOfPoints, index, sr, debug, buffer);

             envelope = aPack.envelope;
             notes = aPack.notes;
             pitch = aPack.pitch;
             peakSize = aPack.peakSize;
             trackSize = aPack.trackSize;
             output = buffer;

        }

        public void clear()
        {
            Sigmund_clear0(this.m_Sigmund);

        }

        public int getMode()
        {
            return Sigmund_getMode0(this.m_Sigmund);
        }

        public double getNumOfPoints()
        {
            return Sigmund_getNumOfPoints0(this.m_Sigmund);
        }

        public double getHop()
        {
            return Sigmund_getHop0(this.m_Sigmund);
        }

        public double getNumOfPeaks()
        {
            return Sigmund_getNumOfPeaks0(this.m_Sigmund);
        }

        public double getMaxFrequency()
        {
            return Sigmund_getMaxFrequency0(this.m_Sigmund);
        }

        public double getVibrato()
        {
            return Sigmund_getVibrato0(this.m_Sigmund);
        }

        public double getStableTime()
        {
            return Sigmund_getStableTime0(this.m_Sigmund);
        }

        public double getMinPower()
        {
            return Sigmund_getMinPower0(this.m_Sigmund);
        }

        public double getGrowth()
        {
            return Sigmund_getGrowth0(this.m_Sigmund);
        }

        public int getPeakColumnNumber()
        {
            return Sigmund_getPeakColumnNumber0(this.m_Sigmund);
        }

        public int getTrackColumnNumber()
        {
            return Sigmund_getTrackColumnNumber0(this.m_Sigmund);
        }
        public int getPeakBool()
        {
            return Sigmund_getPeakBool0(this.m_Sigmund);
        }

        public int getTrackBool()
        {
            return Sigmund_getTrackBool0(this.m_Sigmund);
        }

        #endregion Wrapper Methods
    }

}