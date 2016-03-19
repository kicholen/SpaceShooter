public class RemoveEnemyAction : ILevelAction {
    EnemySpawnModel model;

    public RemoveEnemyAction(EnemySpawnModel model) {
        this.model = model;
    }

    public void Execute(LevelModelComponent component) {
        component.enemies.Remove(model);
    }
}
