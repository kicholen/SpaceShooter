using System;
using UnityEngine;

public class ChangeEnemyScoreAction : IEnemyModelCmpAction
{
    int score;

    public ChangeEnemyScoreAction(string score)
    {
        try
        {
            this.score = Convert.ToInt16(score);
        }
        catch (FormatException exception)
        {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(EnemyModel model)
    {
        model.score = score;
    }
}