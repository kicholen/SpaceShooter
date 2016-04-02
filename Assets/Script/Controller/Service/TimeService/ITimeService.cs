using System;

public interface ITimeService : Updateable
{
    long Now { get; }
    void RegisterCall(Action callback, long time);
    void UnregisterCall(Action callback);
}
