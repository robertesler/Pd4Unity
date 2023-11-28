# Pd4Unity
Pd++ for Unity (Pd4Unity) is a C# library that interfaces the Pure Data signal processing object's code with Unity's API.  This library can be used to create real-time audio, synthesis and procedural audio techniques in Unity.

# Copyright
This software is copyrighted by Robert Esler, 2023.  

# Author(s)
Pd4Unity is written by Robert Esler.  Pd++ is written by Robert Esler.  Pure Data is written by Miller Puckette and others:  see https://github.com/pure-data/pure-data

# Versions (Win/MacOS)
This version of the library right now works on Windows 10/11 and MacOS (13.x).  The .dll in the /libs folder are built for Windows, the .dylibs are for MacOS.  There are plans to test this on iOS and Android.  There is a Processing version of this same library that works for Android: https://github.com/robertesler/Pd4P3

# Tutorials
More to come...

# Pd++
Pd++ is a standalone C++ library based on the signal objects of Pure Data. The dynamic library used for this distribution is based on that.  You can compile it directly for your architecture if you want more advanced usage. More information can be found here: https://bitbucket.org/resler/pd/src/master/

# Pure Data to Pd4Unity object table
These are the Pd objects emulated in Pd4Unity.
| Class    |   Pd Object |
 --------- | ------------ 
 BandPass |   [bp~] 
BiQuad    |  [biquad~]
BobFilter  | [bob~]
Cosine     | [cos~]
Delay      | [delwrite~] and [delread~]
Envelope   | [env~]
HighPass   | [hip~]
Line       | [line~]
LowPass    | [lop~]
Noise      | [noise~]
Oscillator | [osc~]
Phasor     | [phasor~]
ReadSoundFile | [readsf~]*
RealPole | [rpole~]
RealZero | [rzero~]
RealZeroReverse | [rzero_rev~]
ComplexPole | [cpole~]
ComplexZero | [czero~]
ComplexZeroReverse | [czero_rev~]
SampleHold | [samphold~]
Sigmund    | [sigmund~]
SlewLowPass | [slop~]
SoundFiler | [soundfiler~]
TabRead4   | [tabread4]
Threshold  | [threshold~]  
VariableDelay | [vd~] and [delwrite~]
VoltageControlFilter | [vcf~]
WriteSoundFile | [writesf~]*
cFFT     |   [fft~]
cIFFT    |   [ifft~]
rFFT      |  [rfft~]
rIFFT     |  [rifft~]
PdMaster, which is the superclass to all Pd4P3 classes, also has a few utility methods
dbtorms() |  [dbtorms]
rmstodb() |  [rmstodb]
mtof()    |  [mtof]
ftom()    |  [ftom]
powtodb() |  [powtodb]
dbtopow() | [dbtopow]

# Limitations
So far with my testing it appears when the Unity function ```OnAudioFilterRead(float [] data, int channels)``` is used to pass our audio buffer it is subject to the C# Garbage Collector.  This means there may be dropouts.  When used in the Editor the dropouts can be noticeable to very noticeable.  When used within a built game, it appears these dropouts are minimized quite a bit.  This library is not meant to be a high-performance audio engine like FMOD or Wwise, but instead show people how to create procedural audio routines without having to do a lot of heavy lifting for the user.  


