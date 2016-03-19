using System;
using Entitas;
using UnityEngine;

public class EnemyChangeTimeAction : IEnemyWeaponParameterAction {
    float time;

    public EnemyChangeTimeAction(string time) {
        try {
            this.time = (float)Convert.ToDouble(time);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.time = time;
        if (entity.hasCircleMissileSpawner)
            entity.circleMissileSpawner.time = time;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.time = time;
        if (entity.hasDispersionMissileSpawner)
            entity.dispersionMissileSpawner.time = time;
        if (entity.hasHomeMissileSpawner)
            entity.homeMissileSpawner.time = time;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.time = time;
        if (entity.hasMissileSpawner)
            entity.missileSpawner.time = time;
        if (entity.hasTargetMissileSpawner)
            entity.targetMissileSpawner.time = time;
    }
}