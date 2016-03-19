using System;

public class ChangeEnemyTypeAction : IEnemyAction {
    int type;

    public ChangeEnemyTypeAction(string type) {
        this.type = Convert.ToInt16(type);
    }

    public void Execute(EnemySpawnModel model) {
        model.type = type;
    }
}