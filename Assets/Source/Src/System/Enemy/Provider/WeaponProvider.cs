using Entitas;
using UnityEngine;

public class WeaponProvider
{
    DifficultyControllerComponent difficultyController;

    public void SetController(DifficultyControllerComponent difficultyController)
    {
        this.difficultyController = difficultyController;
    }

    public void provide(Entity e, int damage, EnemyModel component)
    {
        damage = (int)(damage * getDamageFactor());

        switch (component.weapon)
        {
            case WeaponTypes.Circle:
                e.AddCircleMissileSpawner(component.amount, damage, component.time, component.spawnDelay, component.weaponResource,
                    component.velocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.CircleRotated:
                e.AddCircleMissileRotatedSpawner(component.amount, damage, component.waves, component.angle, component.angleOffset,
                    component.time, component.spawnDelay, component.weaponResource, component.velocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Dispersion:
                e.AddDispersionMissileSpawner(component.time, damage, component.spawnDelay, component.angle, component.weaponResource,
                    component.velocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Home:
                e.AddHomeMissileSpawner(component.time, component.spawnDelay, damage, component.weaponResource, component.velocity * getMissileSpeedFactor(),
                    component.startVelocity, component.followDelay, component.selfDestructionDelay, CollisionTypes.Enemy);
                break;
            case WeaponTypes.Laser:
                e.AddLaserSpawner(0.0f, component.velocity, component.velocity, component.angle, Vector2.up, CollisionTypes.Enemy, damage, component.weaponResource, null);
                break;
            case WeaponTypes.Multiple:
                e.AddMultipleMissileSpawner(component.amount, damage, 0, component.timeDelay, component.delay, component.time,
                    component.spawnDelay, component.weaponResource, component.randomPositionOffsetX, component.startVelocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Single:
                e.AddMissileSpawner(component.time, damage, component.spawnDelay, component.weaponResource, component.startVelocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Target:
                e.AddTargetMissileSpawner(component.time, damage, component.spawnDelay, component.weaponResource, component.velocity * getMissileSpeedFactor(),
                    CollisionTypes.Enemy, CollisionTypes.Player);
                break;
        }
    }

    float getDamageFactor()
    {
        return (difficultyController.dmgBoostPercent + 100.0f) / 100.0f;
    }

    float getMissileSpeedFactor()
    {
        return (difficultyController.missileSpeedBoostPercent + 100.0f) / 100.0f;
    }
}
