using Entitas;

public class EnemyWeaponActionExecutor {
    Entity entity;
    EnemyModel component;

    public EnemyWeaponActionExecutor(Entity entity, EnemyModel component) {
        this.entity = entity;
        this.component = component;
    }

    public void Execute(IEnemyWeaponAction modifier) {
        modifier.Execute(entity, component);
    }
}