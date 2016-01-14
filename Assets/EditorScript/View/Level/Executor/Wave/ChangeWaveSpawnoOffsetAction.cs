using System;

public class ChangeWaveSpawnOffsetAction : IWaveAction {
    float spawnOffset;

    public ChangeWaveSpawnOffsetAction(string count) {
        this.spawnOffset = (float)Convert.ToDouble(spawnOffset);
    }

    public void Execute(WaveModel model) {
        model.spawnOffset = spawnOffset;
    }
}