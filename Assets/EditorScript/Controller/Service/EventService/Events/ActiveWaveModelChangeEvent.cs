public class ActiveWaveModelChangeEvent : GameEvent {
    public WaveModel model;

    public ActiveWaveModelChangeEvent(WaveModel model) {
        this.model = model;
    }
}