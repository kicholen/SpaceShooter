using Entitas;

public class DispersionWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity) {
        base.Execute(entity);
        entity.AddDispersionMissileSpawner(1.0f, 100, 0.2f, 10, ResourceWithColliders.MissileEnemy, 2.0f, CollisionTypes.Enemy);
    }
}