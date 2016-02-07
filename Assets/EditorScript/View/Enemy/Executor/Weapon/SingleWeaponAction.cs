using Entitas;

public class SingleWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModelComponent model) {
        base.Execute(entity, model);
        entity.AddMissileSpawner(model.time, 0, model.spawnDelay, model.weaponResource, model.startVelocity, CollisionTypes.Enemy);
    }
}