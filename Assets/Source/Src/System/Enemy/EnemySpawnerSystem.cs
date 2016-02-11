using Entitas;
using UnityEngine;

public class EnemySpawnerSystem : IExecuteSystem, ISetPool {

	Group _group;
	Group _camera;
	Group _difficulty;
	Pool _pool;
	EnemyFactory _factory;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.EnemySpawner);
		_camera = pool.GetGroup(Matcher.Camera);
		_difficulty = pool.GetGroup(Matcher.DifficultyController);
		_factory = new EnemyFactory();
		_factory.SetPool(_pool, _pool.GetGroup(Matcher.PathModel), _pool.GetGroup(Matcher.EnemyModel));
	}
	
	public void Execute() {
		DifficultyControllerComponent difficulty = _difficulty.GetSingleEntity().difficultyController;
		Camera camera = _camera.GetSingleEntity().camera.camera;
		Vector3 cameraPosition = camera.transform.position;
		foreach (Entity e in _group.GetEntities()) {
			spawnIfCan(e, cameraPosition, difficulty);
		}
	}
	
	void spawnIfCan(Entity e, Vector3 cameraPosition, DifficultyControllerComponent difficulty) {
		EnemySpawnerComponent enemySpawner = e.enemySpawner;
		LevelModelComponent levelModel = enemySpawner.model;

		if (levelModel.enemyIndex < levelModel.enemies.Count) {
			EnemyModel enemyModel = levelModel.enemies[levelModel.enemyIndex];
			if (enemyModel.spawnBarrier < cameraPosition.y) {
				levelModel.enemyIndex += 1;
				float healthMultiplier = (difficulty.hpBoostPercent + 100) / 100;
				_factory.CreateEnemyByModel(enemyModel, difficulty.missileSpeedBoostPercent, healthMultiplier);
			}
		}

		if (levelModel.waveIndex < levelModel.waves.Count) {
			WaveModel waveModel = levelModel.waves[levelModel.waveIndex];
			if (waveModel.spawnBarrier < cameraPosition.y) {
				levelModel.waveIndex += 1;
				_pool.CreateEntity()
					.AddWaveSpawner(waveModel.count, waveModel.type, waveModel.spawnOffset, 0.0f, waveModel.speed, waveModel.health, waveModel.path, waveModel.grid, waveModel.damage);
			}
		}
	}
}