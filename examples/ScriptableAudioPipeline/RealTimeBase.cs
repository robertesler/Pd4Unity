public abstract struct RealtimeBase : GeneratorInstance.IRealtime
{
    public bool isFinite => false;
    public bool isRealtime => false;
    public DiscreteTime? length => null;

    public abstract void OnCreate();
    public abstract void OnDispose();
    public abstract void OnUpdate(ref Message message);
    public abstract int OnProcess(in RealtimeContext context, ChannelBuffer buffer);

    public void Update(UpdatedDataContext context, Pipe pipe)
    {
        foreach (var element in pipe.GetAvailableData(context))
        {
            if (element.TryGetData(out Message msg))
                OnUpdate(ref msg);
        }
    }

    public void Dispose() => OnDispose();

    public GeneratorInstance.Result Process(
        in RealtimeContext context,
        Pipe pipe,
        ChannelBuffer buffer,
        GeneratorInstance.Arguments args)
    {
        return OnProcess(context, buffer);
    }
}
