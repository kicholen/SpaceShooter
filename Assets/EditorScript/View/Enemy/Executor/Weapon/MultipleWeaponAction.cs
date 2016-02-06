using Entitas;

public class MultipleWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity) {
        base.Execute(entity);
        entity.AddMultipleMissileSpawner(5, 5, 3, 0.1f, 0.1f, 2.0f, 2.0f, ResourceWithColliders.MissileEnemy, 0.1f, 0.0f, 2.0f, CollisionTypes.Enemy);
    }
}