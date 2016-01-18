using System;

public class ChangeEnemySpeedAction : IEnemyAction {
    float speed;

    public ChangeEnemySpeedAction(string speed) {
        this.speed = (float)Convert.ToDouble(speed);
    }

    public void Execute(EnemyModel model) {
        model.speed = speed;
    }
}