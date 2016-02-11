using Entitas;

public class CreateEnemySystem : IInitializeSystem, ISetPool
{
    Pool pool;

    const int ENEMY_COUNT = 1;

    public void SetPool(Pool pool) {
        this.pool = pool;
    }

    public void Initialize() {
        for (int i = 1; i <= ENEMY_COUNT; i++) {
            pool.CreateEntity()
                .AddComponent(ComponentIds.EnemyModel, Utils.Deserialize<EnemyModelComponent>(i.ToString()));
        }
    }
}