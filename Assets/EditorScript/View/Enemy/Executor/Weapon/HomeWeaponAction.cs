using Entitas;

public class HomeWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModel model) {
        base.Execute(entity, model);
        entity.AddHomeMissileSpawner(model.time, model.spawnDelay, 0, model.weaponResource, model.velocity, model.startVelocity, model.followDelay, model.selfDestructionDelay, CollisionTypes.Enemy);
    }
}