using System;
using UnityEngine;

public class ChangeEnemyPosYAction : IEnemyAction {
    float posY;

    public ChangeEnemyPosYAction(string posY) {
        try {
            this.posY = (float)Convert.ToDouble(posY);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(EnemyModel model) {
        model.posY = posY;
    }
}