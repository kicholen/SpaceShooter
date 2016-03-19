using System;

public class ChangeEnemyPathAction : IEnemyAction {
    int path;

    public ChangeEnemyPathAction(string path) {
        this.path = Convert.ToInt16(path);
    }

    public void Execute(EnemySpawnModel model) {
        model.path = path;
    }
}