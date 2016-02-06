using Entitas;

public class TargetWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity) {
        base.Execute(entity);
        entity.AddTargetMissileSpawner(2.0f, 100, 2.0f, ResourceWithColliders.MissileEnemy, 3.0f, CollisionTypes.Enemy, CollisionTypes.Player);
    }
}