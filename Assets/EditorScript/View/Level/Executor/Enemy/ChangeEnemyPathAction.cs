using System;

public class ChangeEnemyPathAction : IEnemyAction {
    int path;

    public ChangeEnemyPathAction(string path) {
        this.path = Convert.ToInt16(path);
    }

    public void Execute(EnemyModel model) {
        model.path = path;
    }
}