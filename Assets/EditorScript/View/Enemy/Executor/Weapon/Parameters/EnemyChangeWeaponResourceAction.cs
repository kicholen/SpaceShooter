using Entitas;

public class EnemyChangeWeaponResourceAction : IEnemyWeaponParameterAction {
    string weaponResource;

    public EnemyChangeWeaponResourceAction(string weaponResource) {
        this.weaponResource = weaponResource;
    }

    public void Execute(Entity entity, EnemyModelComponent component) {
        component.weaponResource = weaponResource;
        if (entity.hasCircleMissileSpawner)
            entity.circleMissileSpawner.resource = weaponResource;
        if (entity.hasCircleMissileRotatedSpawner)
            entity.circleMissileRotatedSpawner.resource = weaponResource;
        if (entity.hasDispersionMissileSpawner)
            entity.dispersionMissileSpawner.resource = weaponResource;
        if (entity.hasHomeMissileSpawner)
            entity.homeMissileSpawner.resource = weaponResource;
        if (entity.hasMultipleMissileSpawner)
            entity.multipleMissileSpawner.resource = weaponResource;
        if (entity.hasMissileSpawner)
            entity.missileSpawner.resource = weaponResource;
        if (entity.hasTargetMissileSpawner)
            entity.targetMissileSpawner.resource = weaponResource;
    }
}