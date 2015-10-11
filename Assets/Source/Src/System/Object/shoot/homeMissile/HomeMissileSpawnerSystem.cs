using Entitas;
using UnityEngine;

public class HomeMissileSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.CircleMissileSpawner);
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		Debug.Log("HomeMissileSpawnerSystem");
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			HomeMissileSpawnerComponent missile = e.homeMissileSpawner;
			missile.time -= deltaTime;
			if (missile.time < 0.0f) {
				missile.time = missile.spawnDelay;
				spawnMissile(missile, e.position);
			}
		}
	}
	
	void spawnMissile(HomeMissileSpawnerComponent missile, PositionComponent position) {
		_pool.CreateEntity()
			.AddPosition(position.x, position.y)
			.AddVelocity(missile.velocity, missile.velocity)
			.AddHealth(0)
			.AddHomeMissile(missile.target, missile.velocity, 0.0f)
			.AddCollision(missile.collisionType)
			.AddResource(missile.resource);
	}
}