public class ActiveWaveModelChangeEvent : GameEvent {
    public WaveSpawnModel model;

    public ActiveWaveModelChangeEvent(WaveSpawnModel model) {
        this.model = model;
    }
}