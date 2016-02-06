using Entitas;

public class CircleWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity) {
        base.Execute(entity);
        entity.AddCircleMissileSpawner(10, 100, 0.5f, 0.2f, ResourceWithColliders.MissileEnemy, 2.0f, CollisionTypes.Enemy);
    }
}