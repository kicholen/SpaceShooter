using System;
using UnityEngine;

public class ChangeEnemyModelWeaponAction : IEnemyModelCmpAction {
    int weapon;

    public ChangeEnemyModelWeaponAction(string weapon) {
        this.weapon = Convert.ToInt16(weapon);
    }
    public void Execute(EnemyModel model) {
        model.weapon = weapon;
        setDefaultParameters(model);
    }

    void setDefaultParameters(EnemyModel model) {
        model.amount = 5;
        model.time = 0.5f;
        model.spawnDelay = 2.0f;
        model.weaponResource = getDefaultResource(model);
        model.velocity = 2.0f;
        model.angle = 10;
        model.angleOffset = 20;
        model.waves = 5;
        model.startVelocity = new Vector2(0.0f, -2.0f);
        model.followDelay = 1.0f;
        model.selfDestructionDelay = 3.0f;
        model.timeDelay = 2.0f;
        model.delay = 2.0f;
        model.randomPositionOffsetX = 0.2f;
    }

    string getDefaultResource(EnemyModel model) {
        if (model.weapon == WeaponTypes.Home)
            return ResourceWithColliders.MissileEnemyHoming;
        if (model.weapon == WeaponTypes.Laser)
            return Resource.Laser;

        return ResourceWithColliders.MissileEnemy;
    }
}