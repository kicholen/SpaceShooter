using Entitas;

public class SingleWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity) {
        base.Execute(entity);
        entity.AddMissileSpawner(0.0f, 100, 2.5f, ResourceWithColliders.MissileEnemy, 0.0f, 4.0f, CollisionTypes.Enemy);
    }
}