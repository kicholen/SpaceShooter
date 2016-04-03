using System;
using UnityEngine;

public class TimeService : ITimeService
{
    long time;

    public TimeService()
    {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        time = (long)(DateTime.UtcNow - epoch).TotalSeconds;
    }

    public long Now { get { return time + (long)Time.realtimeSinceStartup; } }
}