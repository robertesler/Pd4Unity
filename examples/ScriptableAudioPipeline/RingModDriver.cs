public class RingModDriver 
    : AudioGeneratorBehaviour<RingModRealtime, RingModControl>
{
    [Range(100f, 2000f)]
    public float frequency = 440f;

    private float previous;

    protected override void ConfigureInstance(
        ControlContext context,
        out RingModRealtime realtime,
        out RingModControl control)
    {
        realtime = new RingModRealtime(frequency);
        control = new RingModControl();
    }

    private void Update()
    {
        if (!Mathf.Approximately(frequency, previous))
        {
            SendMessageToInstance(new FrequencyEvent(frequency));
            previous = frequency;
        }
    }
}
