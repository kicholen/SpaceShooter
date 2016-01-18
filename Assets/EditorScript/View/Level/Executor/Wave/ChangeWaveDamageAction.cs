using System;

public class ChangeWaveDamageAction : IWaveAction {
    const int defaultDmg = 10;

    int damage;

    public ChangeWaveDamageAction(string damage) {
        try {
            this.damage = Convert.ToInt16(damage);
        }
        catch (FormatException exception) {
            this.damage = defaultDmg;
        }
    }

    public void Execute(WaveModel model) {
        model.damage = damage;
    }
}