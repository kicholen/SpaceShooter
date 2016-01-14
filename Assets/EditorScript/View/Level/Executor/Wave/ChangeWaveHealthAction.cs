using System;

public class ChangeWaveHealthAction : IWaveAction {
    int health;

    public ChangeWaveHealthAction(string health) {
        this.health = Convert.ToInt16(health);
    }

    public void Execute(WaveModel model) {
        model.health = health;
    }
}