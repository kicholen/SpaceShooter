using Entitas;

public class DispersionWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModelComponent model) {
        base.Execute(entity, model);
        entity.AddDispersionMissileSpawner(model.time, 0, model.spawnDelay, model.angle, model.weaponResource, model.velocity, CollisionTypes.Enemy);
    }
}