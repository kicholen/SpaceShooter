using Entitas;
using System.Collections.Generic;

public class CreateEnemySystem : IInitializeSystem, ISetPool
{
    Pool pool;

    const int ENEMY_COUNT = 28;

    public void SetPool(Pool pool) {
        this.pool = pool;
    }

    public void Initialize()
    {
        Entity e = pool.CreateEntity()
            .AddEnemiesModel(new Dictionary<int, EnemyModel>());
        for (int i = 0; i < ENEMY_COUNT; i++)
        {
            EnemyModel model = Utils.Deserialize<EnemyModel>(i.ToString());
            e.enemiesModel.map.Add(model.type, model);
        }
        createEnemyFactory();
    }

    void createEnemyFactory()
    {
        EnemyFactory factory = new EnemyFactory();
        factory.SetPool(pool);
        pool.CreateEntity()
            .AddEnemyFactory(factory);
    }
}