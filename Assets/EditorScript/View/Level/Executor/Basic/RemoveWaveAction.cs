public class RemoveWaveAction : ILevelAction {
    WaveSpawnModel model;

    public RemoveWaveAction(WaveSpawnModel model) {
        this.model = model;
    }

    public void Execute(LevelModelComponent component) {
        component.waves.Remove(model);
    }
}
