using System;

public class ChangeWaveSpeedAction : IWaveAction {
    float speed;

    public ChangeWaveSpeedAction(string speed) {
        this.speed = (float)Convert.ToDouble(speed);
    }

    public void Execute(WaveModel model) {
        model.speed = speed;
    }
}