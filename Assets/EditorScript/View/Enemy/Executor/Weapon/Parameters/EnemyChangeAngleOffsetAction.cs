using System;
using Entitas;
using UnityEngine;

public class EnemyChangeAngleOffsetAction : IEnemyWeaponParameterAction {
    int angleOffset;

    public EnemyChangeAngleOffsetAction(string angleOffset) {
        try {
            this.angleOffset = Convert.ToInt16(angleOffset);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModelComponent component) {
        component.angleOffset = angleOffset;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.angleOffset = angleOffset;
    }
}