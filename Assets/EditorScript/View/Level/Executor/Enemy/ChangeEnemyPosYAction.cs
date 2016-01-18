using System;

public class ChangeEnemyPosYAction : IEnemyAction {
    float posY;

    public ChangeEnemyPosYAction(string posY) {
        this.posY = (float)Convert.ToDouble(posY);
    }

    public void Execute(EnemyModel model) {
        model.posY = posY;
    }
}