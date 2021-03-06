﻿using Entitas;
using UnityEngine;

public class LaserWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModel model) {
        base.Execute(entity, model);
        entity.AddLaserSpawner(0.0f, model.velocity, model.velocity, model.angle, Vector2.up, CollisionTypes.Enemy, 0, model.weaponResource, null);
    }
}