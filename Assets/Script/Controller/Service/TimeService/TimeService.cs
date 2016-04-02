using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeService : ITimeService
{
    Dictionary<Action, long> calls = new Dictionary<Action, long>();
    List<Action> toBeRemoved = new List<Action>();

    long time;

    public TimeService()
    {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        time = (long)(DateTime.UtcNow - epoch).TotalSeconds;
    }

    public long Now { get { return time; } }

    public void Update()
    {
        time += (long)Time.time;

        checkCalls();
        removeCalls();
    }

    public void RegisterCall(Action callback, long time)
    {
        calls.Add(callback, time);
    }

    public void UnregisterCall(Action callback)
    {
        calls.Remove(callback);
    }

    void checkCalls()
    {
        foreach (KeyValuePair<Action, long> call in calls)
        {
            if (call.Value <= time)
            {
                call.Key();
                toBeRemoved.Add(call.Key);
            }
        }
    }

    void removeCalls()
    {
        for (int i = 0; i < toBeRemoved.Count; i++)
            calls.Remove(toBeRemoved[i]);
        toBeRemoved.Clear();
    }
}