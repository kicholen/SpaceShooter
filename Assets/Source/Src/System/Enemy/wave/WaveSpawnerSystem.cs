using Entitas;
using UnityEngine;

public class WaveSpawnerSystem : IExecuteSystem, ISetPool {

	Pool _pool;
	Group _camera;
	Group _group;
	Group _difficulty;
	Group _time;

	EnemyFactory _factory;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.WaveSpawner);
		_difficulty = pool.GetGroup(Matcher.DifficultyController);
		_time = pool.GetGroup(Matcher.Time);
		_camera = pool.GetGroup(Matcher.Camera);

		_factory = new EnemyFactory();
		_factory.SetPool(_pool, _pool.GetGroup(Matcher.PathModel));
	}
	
	public void Execute() {
		Vector3 cameraPosition = _camera.GetSingleEntity().position.pos;
		DifficultyControllerComponent difficulty = _difficulty.GetSingleEntity().difficultyController;
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		float healthMultiplier = (difficulty.hpBoostPercent + 100) / 100;

		foreach (Entity e in _group.GetEntities()) {
			WaveSpawnerComponent component = e.waveSpawner;
			component.time -= deltaTime; 

			if (component.time < 0.0f) {
				_factory.CreateEnemyByType(component.type, 0.0f, cameraPosition.y, (int)(component.health * healthMultiplier), 
				                           difficulty.missileSpeedBoostPercent, component.path, component.grid, component.speed);
				component.time = component.timeOffset;
				component.count = component.count - 1;
			}
			if (component.count == 0) {
				e.isDestroyEntity = true;
			}
		}
	}
}