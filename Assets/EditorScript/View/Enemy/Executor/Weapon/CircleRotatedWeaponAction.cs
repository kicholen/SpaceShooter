using Entitas;

public class CircleRotatedWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity) {
        base.Execute(entity);
        entity.AddCircleMissileRotatedSpawner(5, 100, 5, 10, 20, 0.5f, 2.0f, ResourceWithColliders.MissileEnemy, 2.0f, CollisionTypes.Enemy);
    }
}