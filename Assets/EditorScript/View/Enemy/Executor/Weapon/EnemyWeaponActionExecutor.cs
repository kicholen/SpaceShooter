using Entitas;

public class EnemyWeaponActionExecutor {
    Entity entity;

    public EnemyWeaponActionExecutor(Entity entity) {
        this.entity = entity;
    }

    public void Execute(IEnemyWeaponAction modifier) {
        modifier.Execute(entity);
    }
}