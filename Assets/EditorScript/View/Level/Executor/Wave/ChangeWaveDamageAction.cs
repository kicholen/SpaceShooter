using System;

public class ChangeWaveDamageAction : IWaveAction {
    int damage;

    public ChangeWaveDamageAction(string damage) {
        this.damage = Convert.ToInt16(damage);
    }

    public void Execute(WaveModel model) {
        model.damage = damage;
    }
}