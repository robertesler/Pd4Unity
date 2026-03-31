//The IAudioGenerator is only available in Unity 6000.3 or newer. 
#if UNITY_6000_3_OR_NEWER
using Unity.Burst;
using Unity.Collections;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Audio.ProcessorInstance;
public class AudioDriver : MonoBehaviour, IAudioGenerator
{
   private AudioSource m_AudioSource;

    //Here is where we instantiate our Music struct.
    private MyMusic music;

    /**********************
    Whatever variables you need to be public should be here.
    ***********************/
    public float freq = 400.0F; 

    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    //Then we pass music to the Realtime struct.
    public GeneratorInstance CreateInstance(
        ControlContext context,
        AudioFormat? nestedConfiguration,
        CreationParameters creationParameters)
        => context.AllocateGenerator(new Realtime(music), new Control());

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
        var message = new MyMusic.MusicEvents(freq, 0);

        // Send frequency change to the control side.
        ControlContext.builtIn.SendMessage(instance, ref message);
   }
}

[BurstCompile(CompileSynchronously = true)]
struct Realtime : GeneratorInstance.IRealtime
{
    /**********************
    Declare all of your control variables needed here.
    ***********************/
    internal float freq; 
    internal float someOtherVariable; //add as many more as you need
    internal MyMusic music;

    /**
    Leave these as is, required by the IRealtime interface.
    **/
    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    public Realtime(MyMusic m)
    {
        /**********************
        Inintialize all of your 
        internal control variables here.
        **********************/
        freq = 400.0F;
        someOtherVariable = 0;
        music = m;
        music.Create();//make sure to create the MyMusic instance
    }

    public void Update(UpdatedDataContext context, Pipe pipe)
    {
        // Iterate over all available events (newer overwrite older).
        foreach (var element in pipe.GetAvailableData(context))
        {
            if (element.TryGetData(out MyMusic.MusicEvents evt))
            {
                /**********************
                For every control variable, update here
                ***********************/
                freq = evt.frequency;
                someOtherVariable = evt.value; //example of another variable, you can add as many as you need
            }
        }
    }

    //Dispose of our MyMusic instance, frees memory on .dll side.
    public void Dispose()
    {
        music.Dispose();
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
        music.setFrequency(freq);
        for (int frame = 0; frame < buffer.frameCount; frame++)
        {
            music.runAlgorithm();//run the algorithm for each frame
            if (buffer.channelCount <= 2)
            {
                buffer[0, frame] = (float)music.outputL;
                buffer[1, frame] = (float)music.outputR;
            }
        }

        return buffer.frameCount;
    }
}

/*******************************
All of this is boilerplate, and needs no alteration
********************************/

struct Control : GeneratorInstance.IControl<Realtime>
{
    public void Dispose(ControlContext context, ref Realtime realtime)
    {

        realtime.Dispose();//Dispose of the Realtime instance when the control side is disposed
    }

    public void Update(ControlContext context, Pipe pipe) { }

    public Response OnMessage(ControlContext context, Pipe pipe, Message message)
    {
        if (message.Is<MyMusic.MusicEvents>())
        {
            pipe.SendData(context, message.Get<MyMusic.MusicEvents>());

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
#endif

