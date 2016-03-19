using Entitas;
using UnityEngine;

public class EnemyChangeStartVelocityAction : IEnemyWeaponParameterAction {
    Vector2 startVelocity;

    public EnemyChangeStartVelocityAction(Vector2 startVelocity) {
        this.startVelocity = startVelocity;
    }

    public void Execute(Entity entity, EnemyModel component) {
        component.startVelocity = startVelocity;
        if (entity.hasHomeMissileSpawner)
            entity.homeMissileSpawner.startVelocity = startVelocity;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.startVelocity = startVelocity;
        if (entity.hasMissileSpawner)
            entity.missileSpawner.velocity = startVelocity;
    }
}