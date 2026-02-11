public struct RingModRealtime : RealtimeBase
{
    public float frequency;
    private RingMod ringMod;

    public RingModRealtime(float f)
    {
        frequency = f;
        ringMod = new RingMod();
        ringMod.Create();
    }

    public override void OnCreate() { }

    public override void OnDispose()
    {
        ringMod.Dispose();
    }

    public override void OnUpdate(ref Message message)
    {
        if (message.Is<FrequencyEvent>())
            frequency = message.Get<FrequencyEvent>().value;
    }

    public override int OnProcess(in RealtimeContext context, ChannelBuffer buffer)
    {
        for (int frame = 0; frame < buffer.frameCount; frame++)
        {
            float s = (float)ringMod.perform(frequency);
            for (int ch = 0; ch < buffer.channelCount; ch++)
                buffer[ch, frame] = s;
        }

        return buffer.frameCount;
    }
}
