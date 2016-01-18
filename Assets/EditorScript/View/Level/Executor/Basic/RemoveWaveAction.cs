public class RemoveWaveAction : ILevelAction {
    WaveModel model;

    public RemoveWaveAction(WaveModel model) {
        this.model = model;
    }

    public void Execute(LevelModelComponent component) {
        component.waves.Remove(model);
    }
}
