using System;
using Entitas;
using UnityEngine;

public class EnemyChangeAngleAction : IEnemyWeaponParameterAction {
    int angle;

    public EnemyChangeAngleAction(string angle) {
        try {
            this.angle = Convert.ToInt16(angle);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModelComponent component) {
        component.angle = angle;
        if (entity.hasDispersionMissileSpawner)
            entity.dispersionMissileSpawner.angle = angle;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.angle = angle;
    }
}