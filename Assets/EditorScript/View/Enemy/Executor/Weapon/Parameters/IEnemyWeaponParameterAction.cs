using Entitas;

public interface IEnemyWeaponParameterAction {
    void Execute(Entity entity, EnemyModel component);
}
