using Entitas;

public class EnemyWeaponParameterActionExecutor {
    Entity entity;
    EnemyModel component;

    public EnemyWeaponParameterActionExecutor(Entity entity, EnemyModel component) {
        this.entity = entity;
        this.component = component;
    }

    public void Execute(IEnemyWeaponParameterAction modifier) {
        modifier.Execute(entity, component);
    }
}