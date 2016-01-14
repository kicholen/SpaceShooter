public class ActiveEnemyModelChangeEvent : GameEvent {
    public EnemyModel model;

    public ActiveEnemyModelChangeEvent(EnemyModel model) {
        this.model = model;
    }
}
