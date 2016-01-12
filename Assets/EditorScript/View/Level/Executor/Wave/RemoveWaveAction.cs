public class RemoveWaveAction {
    WaveModel modelToBeRemoved;

    public RemoveWaveAction(WaveModel model) {
        modelToBeRemoved = model;
    }

    public void Execute(LevelModelComponent component) {
        component.waves.Remove(modelToBeRemoved);
    }
}
