using System;
using UnityEngine;

public class ChangeEnemySpeedAction : IEnemyAction {
    float speed;

    public ChangeEnemySpeedAction(string speed) {
        try {
            this.speed = (float)Convert.ToDouble(speed);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(EnemySpawnModel model) {
        model.speed = speed;
    }
}