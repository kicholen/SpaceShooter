using System;
using UnityEngine;

public class ChangeEnemyDamageAction : IEnemyAction {
    const int defaultDmg = 10;

    int damage;

    public ChangeEnemyDamageAction(string damage) {
        try {
            this.damage = Convert.ToInt16(damage);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
            this.damage = defaultDmg;
        }
    }

    public void Execute(EnemySpawnModel model) {
        model.damage = damage;
    }
}