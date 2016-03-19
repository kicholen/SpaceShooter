using Entitas;

public class TargetWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModel model) {
        base.Execute(entity, model);
        entity.AddTargetMissileSpawner(model.time, 0, model.spawnDelay, model.weaponResource, model.velocity, CollisionTypes.Enemy, CollisionTypes.Player);
    }
}