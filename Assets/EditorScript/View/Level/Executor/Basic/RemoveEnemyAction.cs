public class RemoveEnemyAction : ILevelAction {
    EnemyModel model;

    public RemoveEnemyAction(EnemyModel model) {
        this.model = model;
    }

    public void Execute(LevelModelComponent component) {
        component.enemies.Remove(model);
    }
}
