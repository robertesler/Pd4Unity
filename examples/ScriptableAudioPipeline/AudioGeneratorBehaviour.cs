#if UNITY_6000_3_OR_NEWER
using Unity.Burst;
using Unity.Collections;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Audio.ProcessorInstance;

public abstract class AudioGeneratorBehaviour<TRealtime, TControl> 
    : MonoBehaviour, IAudioGenerator
    where TRealtime : struct, GeneratorInstance.IRealtime
    where TControl : struct, GeneratorInstance.IControl<TRealtime>
{
    protected AudioSource audioSource;

    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    protected abstract void ConfigureInstance(
        ControlContext context,
        out TRealtime realtime,
        out TControl control);

    public GeneratorInstance CreateInstance(
        ControlContext context,
        AudioFormat? nestedConfiguration,
        CreationParameters creationParameters)
    {
        ConfigureInstance(context, out var realtime, out var control);
        return context.AllocateGenerator(realtime, control);
    }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected void SendMessageToInstance<T>(T message) where T : struct
    {
        var instance = audioSource.generatorInstance;

        if (!ControlContext.builtIn.Exists(instance))
            return;

        ControlContext.builtIn.SendMessage(instance, ref message);
    }
}

#endif
