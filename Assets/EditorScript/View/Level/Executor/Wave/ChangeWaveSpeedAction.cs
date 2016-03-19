using System;
using UnityEngine;

public class ChangeWaveSpeedAction : IWaveAction {
    float speed;

    public ChangeWaveSpeedAction(string speed) {
        try {
            this.speed = (float)Convert.ToDouble(speed);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(WaveSpawnModel model) {
        model.speed = speed;
    }
}