using Entitas;
using UnityEngine;

public class WaveSpawnerSystem : IExecuteSystem, ISetPool {

	Pool _pool;
	Group _group;
	Group _difficulty;
	Group _time;

	EnemyFactory _factory;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.WaveSpawner);
		_difficulty = pool.GetGroup(Matcher.DifficultyController);
		_time = pool.GetGroup(Matcher.Time);

		_factory = new EnemyFactory();
		_factory.SetPool(_pool, _pool.GetGroup(Matcher.PathModel));
	}
	
	public void Execute() {
		DifficultyControllerComponent difficulty = _difficulty.GetSingleEntity().difficultyController;
		float deltaTime = _time.GetSingleEntity().time.deltaTime;

		foreach (Entity e in _group.GetEntities()) {
			WaveSpawnerComponent component = e.waveSpawner;
			component.time -= deltaTime; 

			if (component.time < 0.0f) {
				_factory.CreateEnemyByType(component.type, component.position, new Vector2(0.0f, 0.0f), component.health, difficulty.missileSpeedBoostPercent, component.path);
				component.time = component.timeOffset;
				component.count = component.count - 1;
			}
			if (component.count == 0) {
				e.isDestroyEntity = true;
			}
		}
	}
}