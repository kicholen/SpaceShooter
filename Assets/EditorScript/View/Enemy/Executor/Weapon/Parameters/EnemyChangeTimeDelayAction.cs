using System;
using Entitas;
using UnityEngine;

public class EnemyChangeTimeDelayAction : IEnemyWeaponParameterAction {
    float timeDelay;

    public EnemyChangeTimeDelayAction(string timeDelay) {
        try {
            this.timeDelay = (float)Convert.ToDouble(timeDelay);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModelComponent component) {
        component.timeDelay = timeDelay;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.timeDelay = timeDelay;
    }
}