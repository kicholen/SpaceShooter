using Entitas;

public interface IEnemyWeaponAction {
    void Execute(Entity entity, EnemyModel component);
}
