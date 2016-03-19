using System;
using Entitas;
using UnityEngine;

public class EnemyChangeWavesAction : IEnemyWeaponParameterAction {
    int waves;

    public EnemyChangeWavesAction(string waves) {
        try {
            this.waves = Convert.ToInt16(waves);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.waves = waves;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.waves = waves;
    }
}