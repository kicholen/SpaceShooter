using Entitas;

public interface IEnemyWeaponParameterAction {
    void Execute(Entity entity, EnemyModelComponent component);
}
