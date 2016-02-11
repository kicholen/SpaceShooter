using System;
using UnityEngine;

public class ChangeWaveSpawnBarrierAction : IWaveAction
{
    float spawnBarrier;

    public ChangeWaveSpawnBarrierAction(string spawnBarrier)
    {
        try
        {
            this.spawnBarrier = (float)Convert.ToDouble(spawnBarrier);
        }
        catch (FormatException exception)
        {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(WaveModel model)
    {
        model.spawnBarrier = spawnBarrier;
    }
}