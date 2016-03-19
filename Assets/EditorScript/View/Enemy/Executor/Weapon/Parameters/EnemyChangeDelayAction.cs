using System;
using Entitas;
using UnityEngine;

public class EnemyChangeDelayAction : IEnemyWeaponParameterAction {
    float delay;

    public EnemyChangeDelayAction(string delay) {
        try {
            this.delay = (float)Convert.ToDouble(delay);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.delay = delay;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.delay = delay;
    }
}