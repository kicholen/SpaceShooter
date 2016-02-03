using System;

public class ChangeWaveSpawnOffsetAction : IWaveAction {
    float spawnOffset;

    public ChangeWaveSpawnOffsetAction(string count) {
        this.spawnOffset = (float)Convert.ToDouble(count);
    }

    public void Execute(WaveModel model) {
        model.spawnOffset = spawnOffset;
    }
}