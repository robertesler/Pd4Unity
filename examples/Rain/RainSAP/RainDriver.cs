//The IAudioGenerator is only available in Unity 6000.3 or newer. 
#if UNITY_6000_3_OR_NEWER
using Unity.Burst;
using Unity.Collections;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Audio.ProcessorInstance;


public class RainDriver : MonoBehaviour, IAudioGenerator
{
    private AudioSource m_AudioSource;

    //Here is where we instantiate our Rain struct.
    private sRain rain;

    /**********************
    Whatever variables you need to be public should be here.
    ***********************/
    public float intensity = 50.0F;

    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    /*
    //I used this to test that my sRain, sDrop, sGlassWindow were properly implemented
    static void RequiresUnmanaged<T>()
    where T : unmanaged
    {
    }
    */

    //Then we pass music to the Realtime struct.
    public GeneratorInstance CreateInstance(
        ControlContext context,
        AudioFormat? nestedConfiguration,
        CreationParameters creationParameters)
        => context.AllocateGenerator(new r_Realtime(rain), new r_Control());

    private void Awake()
    {
        // Expects an AudioSource on the same GameObject.
        m_AudioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        // Access the instance via the AudioSource.
        var instance = m_AudioSource.generatorInstance;

        // Guard the handle: instance may be missing or have been destroyed, if the audio source was stopped elsewhere.
        if (!ControlContext.builtIn.Exists(instance))
            return;

        /*********************
        You can pass whatever variable you need to 
        the MusicEvents constructor. 
        Or none at all, it's up to you.  
        See MyMusic.MusicEvents for details.
        **********************/
        var message = new RainEvents(intensity, 0);

        // Send frequency change to the control side.
        ControlContext.builtIn.SendMessage(instance, ref message);
    }


}

public readonly struct RainEvents
    {
        /**********************
        Add any control variables you need 
        in the MusicEvents struct.
        Call these whatever you want.
        They can be the same as the public variables in MyMusic, 
        or they can be different.
        ***********************/
        public readonly float value;
        public readonly float intensity; //same as below
        public RainEvents(float f, float v)
        {
            this.intensity = f; 
            this.value = v;
        } 
    }

[BurstCompile(CompileSynchronously = true)]
struct r_Realtime : GeneratorInstance.IRealtime
{
    /**********************
    Declare all of your control variables needed here.
    ***********************/
    internal float intensity; 
    internal sRain rain;

    /**
    Leave these as is, required by the IRealtime interface.
    **/
    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    public r_Realtime(sRain r)
    {
        /**********************
        Inintialize all of your 
        internal control variables here.
        **********************/
        intensity = 50.0F;
        rain = r;
        rain.Create();//make sure to create the MyMusic instance
    }

    public void Update(UpdatedDataContext context, Pipe pipe)
    {
        // Iterate over all available events (newer overwrite older).
        foreach (var element in pipe.GetAvailableData(context))
        {
            if (element.TryGetData(out RainEvents evt))
            {
                /**********************
                For every control variable, update here
                ***********************/
                intensity = evt.intensity;
            }
        }
    }

    //Dispose of our instance, frees memory on .dll side.
    public void Dispose()
    {
        rain.Dispose();
    }
    
    public GeneratorInstance.Result Process(
        in RealtimeContext context,
        Pipe pipe,
        ChannelBuffer buffer,
        GeneratorInstance.Arguments args)
    {
        /************************************
        Set our control variables outside the loop.
        You can add as many getter/setter functions as needed. 
        *************************************/
        // music.setFrequency(freq);
        for (int frame = 0; frame < buffer.frameCount; frame++)
        {
            double output = rain.perform();//perform the algorithm for each frame
            rain.setIntensity(this.intensity);
            if (buffer.channelCount <= 2)
            {
                buffer[0, frame] = (float)output;
                buffer[1, frame] = (float)output;
            }
        }

        return buffer.frameCount;
    }
}

/*******************************
All of this is boilerplate, and needs no alteration
********************************/

struct r_Control : GeneratorInstance.IControl<r_Realtime>
{
    public void Dispose(ControlContext context, ref r_Realtime realtime)
    {

        realtime.Dispose();//Dispose of the Realtime instance when the control side is disposed
    }

    public void Update(ControlContext context, Pipe pipe) { }

    public Response OnMessage(ControlContext context, Pipe pipe, Message message)
    {
        if (message.Is<RainEvents>())
        {
            pipe.SendData(context, message.Get<RainEvents>());

            return Response.Handled;
        }

        return Response.Unhandled;
    }

    public void Configure(
        ControlContext context,
        ref r_Realtime realtime,
        in AudioFormat format,
        out GeneratorInstance.Setup setup,
        ref GeneratorInstance.Properties properties)
    {
        setup = new GeneratorInstance.Setup(AudioSpeakerMode.Mono, format.sampleRate);
    }
}
#endif