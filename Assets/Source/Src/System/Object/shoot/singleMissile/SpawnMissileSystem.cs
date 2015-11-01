using Entitas;
using UnityEngine;

public class SpawnMissileSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;

	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.MissileSpawner);
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			MissileSpawnerComponent missile = e.missileSpawner;
			missile.time -= deltaTime;
			if (missile.time < 0.0f) {
				missile.time = missile.spawnDelay;
				spawnMissile(missile, e.position.pos);
			}
		}
	}
	
	void spawnMissile(MissileSpawnerComponent missile, Vector2 position) {
		_pool.CreateEntity()
			.AddPosition(new Vector2().Set(position))
			.AddVelocity(new Vector2(missile.velocityX, missile.velocityY))
			.AddHealth(0)
			.AddCollision(missile.collisionType)
			.AddResource(missile.resource);
	}

}