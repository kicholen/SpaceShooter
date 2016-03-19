using System;
using UnityEngine;

public class ChangeEnemySpawnBarrierAction : IEnemyAction
{
    float spawnBarrier;

    public ChangeEnemySpawnBarrierAction(string spawnBarrier)
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

    public void Execute(EnemySpawnModel model)
    {
        model.spawnBarrier = spawnBarrier;
    }
}
