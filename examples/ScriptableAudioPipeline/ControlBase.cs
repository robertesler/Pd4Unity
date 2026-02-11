public abstract struct ControlBase<TRealtime> 
    : GeneratorInstance.IControl<TRealtime>
    where TRealtime : struct, GeneratorInstance.IRealtime
{
    public abstract void OnConfigure(
        ControlContext context,
        ref TRealtime realtime,
        in AudioFormat format,
        out GeneratorInstance.Setup setup,
        ref GeneratorInstance.Properties properties);

    public abstract Response OnMessage(ControlContext context, Pipe pipe, Message message);

    public virtual void Update(ControlContext context, Pipe pipe) { }

    public virtual void Dispose(ControlContext context, ref TRealtime realtime)
    {
        realtime.Dispose();
    }

    public void Configure(
        ControlContext context,
        ref TRealtime realtime,
        in AudioFormat format,
        out GeneratorInstance.Setup setup,
        ref GeneratorInstance.Properties properties)
    {
        OnConfigure(context, ref realtime, format, out setup, ref properties);
    }
}
