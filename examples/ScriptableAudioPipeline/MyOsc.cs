
using System.Runtime.InteropServices;
using System;

/*
 This struct is a wrapper around a native PdPlusPlus oscillator.
It show an example of how to use P/Invoke to call into native code from C#.
 */

namespace MyPd
{

    public struct MyOsc
    {
        //FYI: I didn't include the iOS DllImport
        [DllImport("pdplusplusUnity")]
        public static extern IntPtr Oscillator_allocate0();

        [DllImport("pdplusplusUnity")]
        public static extern void Oscillator_free0(IntPtr ptr);

        [DllImport("pdplusplusUnity")]
        public static extern double Oscillator_perform0(IntPtr ptr, double f);

        [DllImport("pdplusplusUnity")]
        public static extern void Oscillator_setPhase0(IntPtr ptr, double f);

        private IntPtr m_Oscillator;

        //Can't have a constructor in a struct that uses P/Invoke, so we use Create() instead.
        public void Create()
        {
            m_Oscillator = Oscillator_allocate0();
        }

        //No need for IDisposable interface, just call Dispose() explicitly.
        public void Dispose()
        {
            if (this.m_Oscillator != IntPtr.Zero)
            {
                Oscillator_free0(this.m_Oscillator);
                this.m_Oscillator = IntPtr.Zero;
            }
        }

        #region Wrapper Methods
        public double perform(double freq)
        {
            return Oscillator_perform0(this.m_Oscillator, freq);
        }

        public void setPhase(double ph)
        {
            Oscillator_setPhase0(this.m_Oscillator, ph);
        }
        #endregion Wrapper Methods
    }

}
