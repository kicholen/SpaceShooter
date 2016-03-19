using System;
using UnityEngine;

public class ChangeWaveDamageAction : IWaveAction {
    const int defaultDmg = 10;

    int damage;

    public ChangeWaveDamageAction(string damage) {
        try {
            this.damage = Convert.ToInt16(damage);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
            this.damage = defaultDmg;
        }
    }

    public void Execute(WaveSpawnModel model) {
        model.damage = damage;
    }
}