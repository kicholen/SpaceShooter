using System;
using Entitas;
using UnityEngine;

public class EnemyChangeSpawnDelayAction : IEnemyWeaponParameterAction {
    float spawnDelay;

    public EnemyChangeSpawnDelayAction(string spawnDelay) {
        try {
            this.spawnDelay = (float)Convert.ToDouble(spawnDelay);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.spawnDelay = spawnDelay;
        if (entity.hasCircleMissileSpawner)
            entity.circleMissileSpawner.spawnDelay = spawnDelay;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.spawnDelay = spawnDelay;
        if (entity.hasDispersionMissileSpawner)
            entity.dispersionMissileSpawner.spawnDelay = spawnDelay;
        if (entity.hasHomeMissileSpawner)
            entity.homeMissileSpawner.spawnDelay = spawnDelay;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.spawnDelay = spawnDelay;
        if (entity.hasMissileSpawner)
            entity.missileSpawner.spawnDelay = spawnDelay;
        if (entity.hasTargetMissileSpawner)
            entity.targetMissileSpawner.spawnDelay = spawnDelay;
    }
}