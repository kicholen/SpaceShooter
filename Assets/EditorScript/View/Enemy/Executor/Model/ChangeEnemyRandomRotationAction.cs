using System;
using UnityEngine;

public class ChangeEnemyRandomRotationAction : IEnemyModelCmpAction
{
    float randomRotation;

    public ChangeEnemyRandomRotationAction(string randomRotation)
    {
        try
        {
            this.randomRotation = (float)Convert.ToDouble(randomRotation);
        }
        catch (FormatException exception)
        {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(EnemyModel model)
    {
        model.randomRotation = randomRotation;
    }
}