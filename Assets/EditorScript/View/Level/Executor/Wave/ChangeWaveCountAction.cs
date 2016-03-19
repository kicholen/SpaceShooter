using System;
using UnityEngine;

public class ChangeWaveCountAction : IWaveAction {
    int count;

    public ChangeWaveCountAction(string count) {
        try {
            this.count = Convert.ToInt16(count);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(WaveSpawnModel model) {
        model.count = count;
    }
}