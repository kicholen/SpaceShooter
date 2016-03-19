using Entitas;

public class CircleWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModel model) {
        base.Execute(entity, model);
        entity.AddCircleMissileSpawner(model.amount, 0, model.time, model.spawnDelay, model.weaponResource, model.velocity, CollisionTypes.Enemy);
    }
}