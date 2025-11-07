using Unity.Burst;
using Unity.Collections;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.Audio;
using MyPd;//This simulates our PdPlusPlus oscillator
using static UnityEngine.Audio.ProcessorInstance;

/*
 This is an example of how to use the new Scriptable Audio Pipeline API with the PdPlusPlus library.
 Currently, the C# wrapper has only been tested with our oscillator, but should this work,
 Then I will refactor the rest of the library to use similar patterns.

 See the RingMod struct below. 
*/

public class Driver : MonoBehaviour, IAudioGenerator
{
    private AudioSource m_AudioSource;

    //Here is where we instantiate our RingMod struct.
    private RingMod ringMod;


    [Range(100f, 2000f)]
    public float frequency = 440.0f; // A4

    private float m_PreviousFrequency;

    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    //Then we pass it to the Realtime struct when creating the generator instance.
    public GeneratorInstance CreateInstance(
        ControlContext context,
        AudioFormat? nestedConfiguration,
        CreationParameters creationParameters)
        => context.AllocateGenerator(new Realtime(frequency, 48000, ringMod), new Control());

    private void Awake()
    {
        // Expects an AudioSource on the same GameObject.
        m_AudioSource = GetComponent<AudioSource>();      
    }

    private void Update()
    {
        // Early out if unchanged (use Approximate to avoid redundant updates).
        if (Mathf.Approximately(frequency, m_PreviousFrequency))
            return;

        // Access the instance via the AudioSource.
        var instance = m_AudioSource.generatorInstance;

        // Guard the handle: instance may be missing or have been destroyed, if the audio source was stopped elsewhere.
        if (!ControlContext.builtIn.Exists(instance))
            return;

        var message = new FrequencyEvent(frequency);

        // Send frequency change to the control side.
        ControlContext.builtIn.SendMessage(instance, ref message);
        m_PreviousFrequency = frequency;
    }
}

readonly struct FrequencyEvent
{
    public readonly float value;
    public FrequencyEvent(float value) => this.value = value;
}

/*
This is how you would implement a new DSP routine. 
Here we implement a ring modulator using two instances of MyOsc.
Notice how we create and dispose of the MyOsc instances.
This is not much different from how we did it before, except now it's wrapped in a struct.
And there is no constructor or destructor, we have to call Create() and Dispose() explicitly.
This could also be in a separate file.
 */
struct RingMod
{
    private MyOsc osc1;
    private MyOsc osc2;

    public void Create()
    {
        osc1.Create();
        osc2.Create();
    }

    public void Dispose()
    {
        osc1.Dispose();
        osc2.Dispose();
    }

    public double perform(double freq)
    {
        return osc1.perform(freq) * osc2.perform(freq * .1f);
    }

}



[BurstCompile(CompileSynchronously = true)]
struct Realtime : GeneratorInstance.IRealtime
{

    internal float frequency;  // Hz, set from control messages
    internal RingMod ringMod;
    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    public Realtime(float f, float sr, RingMod rm)
    {
        frequency = f;
        ringMod = rm;
        ringMod.Create();//make sure to create the RingMod instance
    }

    public void Update(UpdatedDataContext context, Pipe pipe)
    {
        // Iterate over all available events (newer overwrite older).
        foreach (var element in pipe.GetAvailableData(context))
        {
            if (element.TryGetData(out FrequencyEvent evt))
            {
                frequency = evt.value;
            }

            // Ignore other message types gracefully.
        }
    }

    public void Dispose()
    {
        ringMod.Dispose();//here we dispose of the RingMod instance, which in turn disposes of its MyOsc instances
    }

    public GeneratorInstance.Result Process(
        in RealtimeContext context,
        Pipe pipe,
        ChannelBuffer buffer,
        GeneratorInstance.Arguments args)
    {
        
        for (int frame = 0; frame < buffer.frameCount; frame++)
        {
            for (int ch = 0; ch < buffer.channelCount; ch++)
                buffer[ch, frame] = (float)ringMod.perform(frequency);
        }

        return buffer.frameCount;
    }
}


struct Control : GeneratorInstance.IControl<Realtime>
{
    public void Dispose(ControlContext context, ref Realtime realtime) {
    
        realtime.Dispose();//Dispose of the Realtime instance when the control side is disposed
    }

    public void Update(ControlContext context, Pipe pipe) { }

    public Response OnMessage(ControlContext context, Pipe pipe, Message message)
    {
        if (message.Is<FrequencyEvent>())
        {
            pipe.SendData(context, message.Get<FrequencyEvent>());

            return Response.Handled;
        }

        return Response.Unhandled;
    }

    public void Configure(
        ControlContext context,
        ref Realtime realtime,
        in AudioFormat format,
        out GeneratorInstance.Setup setup,
        ref GeneratorInstance.Properties properties)
    {

        setup = new GeneratorInstance.Setup(AudioSpeakerMode.Mono, format.sampleRate);
    }
}

