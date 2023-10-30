# Pd4Unity
Pd++ for Unity is a C# library that interfaces the Pure Data signal processing objects with Unity's API.  This library can be used to create real-time audio, synthesis and procedural audio techniques in Unity.

#Limitations
So far with my testing it appears when the Unity function ```OnAudioFilterRead(float [] data, int channels)``` is used to pass our audio buffer it is subject to Garbage Collector.  This means there are inevitably dropouts.  When used in the Editor the dropouts can be noticeable to very noticeable.  When used within a built game, it appears these dropouts are minimized quite a bit.  So it would seem that this library is good for shorter audio effects or synthesizers. 

However, I am currently testing whether off loading some of the processing to the C++ side helps with this or not.  See my Wind Generator example.  More to come...
