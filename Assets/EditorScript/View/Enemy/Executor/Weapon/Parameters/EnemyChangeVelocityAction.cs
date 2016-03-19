using System;
using Entitas;
using UnityEngine;

public class EnemyChangeVelocityAction : IEnemyWeaponParameterAction {
    float velocity;

    public EnemyChangeVelocityAction(string velocity) {
        try {
            this.velocity = (float)Convert.ToDouble(velocity);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.velocity = velocity;
        if (entity.hasCircleMissileSpawner)
            entity.circleMissileSpawner.velocity = velocity;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.velocity = velocity;
        if (entity.hasDispersionMissileSpawner)
            entity.dispersionMissileSpawner.velocity = velocity;
        if (entity.hasHomeMissileSpawner)
            entity.homeMissileSpawner.velocity = velocity;
        if (entity.hasTargetMissileSpawner)
            entity.targetMissileSpawner.velocity = velocity;
        if (entity.hasLaserSpawner)
            entity.laserSpawner.maxHeight = velocity;
    }
}