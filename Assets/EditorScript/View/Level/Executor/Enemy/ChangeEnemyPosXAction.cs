using System;
using UnityEngine;

public class ChangeEnemyPosXAction : IEnemyAction {
    float posX;

    public ChangeEnemyPosXAction(string posX) {
        try {
            this.posX = (float)Convert.ToDouble(posX);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(EnemyModel model) {
        model.posX = posX;
    }
}
