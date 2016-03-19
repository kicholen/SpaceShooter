using Entitas;

public class CircleRotatedWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModel model) {
        base.Execute(entity, model);
        entity.AddCircleMissileRotatedSpawner(model.amount, 0, model.waves, model.angle, model.angleOffset, model.time, model.spawnDelay, model.weaponResource, model.velocity, CollisionTypes.Enemy);
    }
}