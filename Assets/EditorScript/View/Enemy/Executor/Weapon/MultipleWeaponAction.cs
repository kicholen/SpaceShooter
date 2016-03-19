using Entitas;

public class MultipleWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModel model) {
        base.Execute(entity, model);
        entity.AddMultipleMissileSpawner(model.amount, 0, 0, model.timeDelay, model.delay, model.time, model.spawnDelay, model.weaponResource, model.randomPositionOffsetX, model.startVelocity, CollisionTypes.Enemy);
    }
}