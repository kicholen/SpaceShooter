using Entitas;
using UnityEngine;

public class WaveSpawnerSystem : IExecuteSystem, ISetPool {
	Group camera;
	Group group;
	Group time;
    Group enemyFactory;

    EnemyFactory factory;

    public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.WaveSpawner);
		time = pool.GetGroup(Matcher.Time);
		camera = pool.GetGroup(Matcher.Camera);
        enemyFactory = pool.GetGroup(Matcher.EnemyFactory);
	}
	
	public void Execute()
    {
        Vector3 cameraPosition = camera.GetSingleEntity().position.pos;
        float deltaTime = time.GetSingleEntity().time.gameDeltaTime;

        foreach (Entity e in group.GetEntities())
        {
            WaveSpawnerComponent component = e.waveSpawner;
            component.time -= deltaTime;

            if (component.time < 0.0f)
            {
                getFactory().CreateEnemyByType(component.type, 0.0f, cameraPosition.y, component.health, component.path, component.grid, component.damage, component.speed);
                component.time = component.timeOffset;
                component.count = component.count - 1;
            }
            if (component.count == 0)
            {
                e.isDestroyEntity = true;
            }
        }
    }

    EnemyFactory getFactory()
    {
        if (factory == null)
            factory = enemyFactory.GetSingleEntity().enemyFactory.factory;
        return factory;
    }
}