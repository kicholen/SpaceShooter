using System;
using Entitas;
using UnityEngine;

public class EnemyChangeFollowDelayAction : IEnemyWeaponParameterAction {
    float followDelay;

    public EnemyChangeFollowDelayAction(string followDelay) {
        try {
            this.followDelay = (float)Convert.ToDouble(followDelay);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.followDelay = followDelay;
        if (entity.hasHomeMissileSpawner)
            entity.homeMissileSpawner.followDelay = followDelay;
    }
}