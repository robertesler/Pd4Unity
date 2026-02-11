public struct RingModControl : ControlBase<RingModRealtime>
{
    public override Response OnMessage(ControlContext context, Pipe pipe, Message message)
    {
        if (message.Is<FrequencyEvent>())
        {
            pipe.SendData(context, message.Get<FrequencyEvent>());
            return Response.Handled;
        }

        return Response.Unhandled;
    }

    public override void OnConfigure(
        ControlContext context,
        ref RingModRealtime realtime,
        in AudioFormat format,
        out GeneratorInstance.Setup setup,
        ref GeneratorInstance.Properties properties)
    {
        setup = new GeneratorInstance.Setup(AudioSpeakerMode.Mono, format.sampleRate);
    }
}
