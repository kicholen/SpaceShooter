using Entitas;
using UnityEngine;

public class EnemySpawnerSystem : IExecuteSystem, ISetPool {
	Pool pool;
	Group group;
	Group camera;
    Group enemyFactory;

    EnemyFactory factory;

    public void SetPool(Pool pool) {
		this.pool = pool;
		group = pool.GetGroup(Matcher.EnemySpawner);
		camera = pool.GetGroup(Matcher.Camera);
        enemyFactory = pool.GetGroup(Matcher.EnemyFactory);
	}
	
	public void Execute() {
        Vector3 cameraPosition = camera.GetSingleEntity().position.pos;
        EnemyFactory factory = getFactory();

        foreach (Entity e in group.GetEntities()) {
			spawnIfCan(e, cameraPosition, factory);
		}
	}
	
	void spawnIfCan(Entity e, Vector3 cameraPosition, EnemyFactory factory) {
		EnemySpawnerComponent enemySpawner = e.enemySpawner;
		LevelModelComponent levelModel = enemySpawner.model;

		if (levelModel.enemyIndex < levelModel.enemies.Count) {
			EnemyModel enemyModel = levelModel.enemies[levelModel.enemyIndex];
			if (enemyModel.spawnBarrier < cameraPosition.y) {
				levelModel.enemyIndex += 1;
                factory.CreateEnemyByModel(enemyModel);
			}
		}

		if (levelModel.waveIndex < levelModel.waves.Count) {
			WaveModel waveModel = levelModel.waves[levelModel.waveIndex];
			if (waveModel.spawnBarrier < cameraPosition.y) {
				levelModel.waveIndex += 1;
				pool.CreateEntity()
					.AddWaveSpawner(waveModel.count, waveModel.type, waveModel.spawnOffset, 0.0f, waveModel.speed, waveModel.health, waveModel.path, waveModel.grid, waveModel.damage);
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