using System;

public class ChangeEnemyPosXAction : IEnemyAction {
    float posX;

    public ChangeEnemyPosXAction(string posX) {
        this.posX = (float)Convert.ToDouble(posX);
    }

    public void Execute(EnemyModel model) {
        model.posX = posX;
    }
}
