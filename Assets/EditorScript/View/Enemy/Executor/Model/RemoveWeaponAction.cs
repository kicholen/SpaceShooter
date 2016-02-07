using Entitas;

public class RemoveWeaponAction : IEnemyWeaponAction {
    public virtual void Execute(Entity entity, EnemyModelComponent component) {
        if (entity.hasCircleMissileSpawner)
            entity.RemoveCircleMissileSpawner();
        if (entity.hasCircleMissileRotatedSpawner)
            entity.RemoveCircleMissileRotatedSpawner();
        if (entity.hasDispersionMissileSpawner)
            entity.RemoveDispersionMissileSpawner();
        if (entity.hasHomeMissileSpawner)
            entity.RemoveHomeMissileSpawner();
        if (entity.hasLaserSpawner)
            entity.RemoveLaserSpawner();
        if (entity.hasMultipleMissileSpawner)
            entity.RemoveMultipleMissileSpawner();
        if (entity.hasMissileSpawner)
            entity.RemoveMissileSpawner();
        if (entity.hasTargetMissileSpawner)
            entity.RemoveTargetMissileSpawner();
    }
}