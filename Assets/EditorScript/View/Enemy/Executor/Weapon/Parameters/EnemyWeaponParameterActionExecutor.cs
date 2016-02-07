using Entitas;

public class EnemyWeaponParameterActionExecutor {
    Entity entity;
    EnemyModelComponent component;

    public EnemyWeaponParameterActionExecutor(Entity entity, EnemyModelComponent component) {
        this.entity = entity;
        this.component = component;
    }

    public void Execute(IEnemyWeaponParameterAction modifier) {
        modifier.Execute(entity, component);
    }
}