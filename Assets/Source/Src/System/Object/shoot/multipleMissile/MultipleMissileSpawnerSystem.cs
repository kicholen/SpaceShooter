using Entitas;
using UnityEngine;

public class MultipleMissileSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;

	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.MultipleMissileSpawner);
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			MultipleMissileSpawnerComponent missile = e.multipleMissileSpawner;
			missile.time -= deltaTime;
			if (missile.time < 0.0f) {
				missile.delay -= deltaTime;
				if (missile.delay < 0.0f) {
					missile.delay = missile.timeDelay;
					spawnMissile(missile, e.position);
					missile.currentAmount--;
					if (missile.currentAmount == 0) {
						missile.time = missile.spawnDelay;
						missile.currentAmount = missile.amount;
					}
				}
			}
		}
	}
	
	void spawnMissile(MultipleMissileSpawnerComponent missile, PositionComponent position) {
		float offsetX = Random.Range(-missile.randomPositionOffsetX, missile.randomPositionOffsetX);
		_pool.CreateEntity()
			.AddPosition(position.x + offsetX, position.y)
			.AddVelocity(missile.velocityX, missile.velocityY)
			.AddHealth(0)
			.AddCollision(missile.collisionType)
			.AddResource(missile.resource);
	}

}