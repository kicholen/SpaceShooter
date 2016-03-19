using System;
using Entitas;
using UnityEngine;

public class EnemyChangeRandomPositionOffsetXAction : IEnemyWeaponParameterAction {
    float randomPositionOffsetX;

    public EnemyChangeRandomPositionOffsetXAction(string randomPositionOffsetX) {
        try {
            this.randomPositionOffsetX = (float)Convert.ToDouble(randomPositionOffsetX);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.randomPositionOffsetX = randomPositionOffsetX;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.randomPositionOffsetX = randomPositionOffsetX;
    }
}
