using Entitas;

public class HomeWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity) {
        base.Execute(entity);
        entity.AddHomeMissileSpawner(0.5f, 0.2f, 100, ResourceWithColliders.MissileEnemyHoming, 2.0f, new UnityEngine.Vector2(1.0f, 1.0f), 0.5f, 5.0f, CollisionTypes.Enemy);
    }
}