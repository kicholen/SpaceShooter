public class ActiveEnemyModelChangeEvent : GameEvent {
    public EnemySpawnModel model;

    public ActiveEnemyModelChangeEvent(EnemySpawnModel model) {
        this.model = model;
    }
}
