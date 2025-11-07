# Pd4Unity
Pd++ for Unity (Pd4Unity) is a C# library that interfaces the Pure Data signal processing object's code with Unity's API.  This library can be used to create real-time audio, synthesis and procedural audio techniques in Unity.  

# How to Get Started
Clone or download the libary.  Drag the Pd4Unity folder into a Unity Project.  Look at the examples for how to use the library.  Pd4Unity is purely C# code.  It cannot read Pure Data patch files, if you are looking for something like that than libpd may be for you: https://github.com/libpd/libpd

# Copyright
This software is copyrighted by Robert Esler, 2024.  

# Author(s)
Pd4Unity is written by Robert Esler and part of the non-profit urbanSTEW.  Pd++ is written by Robert Esler.  Pure Data is written by Miller Puckette and others:  see https://github.com/pure-data/pure-data

# Versions (Win/MacOS)
This version of the library right now works on Windows 10/11 and MacOS (13.x).  The .dll in the /libs folder are built for Windows, the .dylibs are for MacOS.  There are plans to test this on iOS and Android.  There is a Processing version of this same library that works for Android: https://github.com/robertesler/Pd4P3
Unity on Windows may require the Windows Software Development Kit.  If you Unity can't load the .dll then this may be the case. You can install this from Visual Studio via "Tools/Get Tools..." and select "Desktop Development with C++".  
Unity on Mac may have a security issue with the .dylib, you will need to approve the libpdplusplus.dylib in the System Preferences, "Privacy and Security".  If you need to get around this, you can build the pdplusplus dynamic library from the source on your machine.  See https://bitbucket.org/resler/pd/src/master/ for that repository.

# How it Works
Pd4Unity is largely a C# wrapper around a C++ backend compiled as a dynamic library.  These are in the /libs folder for Windows (.dll) and MacOS (.dylib) and come from the Pd++ library.  You could compile Pd++ for almost any architecture.  Included in this distribution are examples on how to use the library.  

As of Unity 6000.3.x there is now an experimental Scriptable Audio Pipeline that is realtime safe and optimized for performance.  This will ultimately replace the `OnAudioFilterRead()` method, but not for quite a while.  I will start refactoring the library so it can work with both soon.  Check back for updates.  For now there is an example in the folder examples/ScriptableAudioPipeline.  

# Tutorials
Here is a short tutorial to get started: https://www.youtube.com/watch?v=l-q-ldqxS_Y&t=25s. There are also examples included with the repository that show how to use the library.  

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
PdMaster, which is the superclass to all PdPlusPlus classes, also has a few utility methods
dbtorms() |  [dbtorms]
rmstodb() |  [rmstodb]
mtof()    |  [mtof]
ftom()    |  [ftom]
powtodb() |  [powtodb]
dbtopow() | [dbtopow]

* ReadSoundFile and WriteSoundFile are experimental.  I recommend using Unity's built-in AudioClip API if trying to use audio files.

# Limitations
So far with my testing it appears when the Unity function ```OnAudioFilterRead(float [] data, int channels)``` is used to pass our audio buffer it is subject to the C# Garbage Collector.  This means there may be dropouts.  When used in the Editor the dropouts can be noticeable to very noticeable.  When used within a built game, it appears these dropouts are minimized quite a bit.  This library is not meant to be a high-performance audio engine like FMOD or Wwise, but instead show people how to create procedural audio routines without having to do a lot of heavy lifting for the user.  If you do want to build a more optimized plug-in for Unity, than Pd++ interfacing with the Unity Plug-In API would be the way to go. 


