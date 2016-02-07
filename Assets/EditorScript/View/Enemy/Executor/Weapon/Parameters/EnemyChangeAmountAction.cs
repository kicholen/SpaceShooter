using System;
using Entitas;
using UnityEngine;

public class EnemyChangeAmountAction : IEnemyWeaponParameterAction {
    int amount;

    public EnemyChangeAmountAction(string amount) {
        try {
            this.amount = Convert.ToInt16(amount);
        }
        catch (FormatException exception) {
            Debug.Log(exception.Message);
        }
    }

    public void Execute(Entity entity, EnemyModelComponent component) {
        component.amount = amount;
        if (entity.hasCircleMissileSpawner)
            entity.circleMissileSpawner.amount = amount;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.amount = amount;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.amount = amount;
    }
}