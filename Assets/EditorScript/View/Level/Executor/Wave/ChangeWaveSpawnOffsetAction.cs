using System;
using UnityEngine;

public class ChangeWaveSpawnOffsetAction : IWaveAction {
    float spawnOffset;

    public ChangeWaveSpawnOffsetAction(string spawnOffset) {
        try {
            this.spawnOffset = (float)Convert.ToDouble(spawnOffset);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(WaveModel model) {
        model.spawnOffset = spawnOffset;
    }
}