using Entitas;

public class EnemyWeaponActionExecutor {
    Entity entity;
    EnemyModelComponent component;

    public EnemyWeaponActionExecutor(Entity entity, EnemyModelComponent component) {
        this.entity = entity;
        this.component = component;
    }

    public void Execute(IEnemyWeaponAction modifier) {
        modifier.Execute(entity, component);
    }
}