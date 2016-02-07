using Entitas;

public class LaserWeaponAction : RemoveWeaponAction {
    public override void Execute(Entity entity, EnemyModelComponent model) {
        base.Execute(entity, model);
        entity.AddLaserSpawner(5.0f, 10.0f, 20.0f, new UnityEngine.Vector2(), CollisionTypes.Enemy, null);
    }
}