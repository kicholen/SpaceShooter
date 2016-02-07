using System;
using Entitas;
using UnityEngine;

public class EnemyChangeSelfDestructDelayAction : IEnemyWeaponParameterAction {
    float selfDestructionDelay;

    public EnemyChangeSelfDestructDelayAction(string selfDestructionDelay) {
        try {
            this.selfDestructionDelay = (float)Convert.ToDouble(selfDestructionDelay);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModelComponent component) {
        component.selfDestructionDelay = selfDestructionDelay;
        if (entity.hasHomeMissileSpawner)
            entity.homeMissileSpawner.selfDestructionDelay = selfDestructionDelay;
    }
}