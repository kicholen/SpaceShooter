using Entitas;
using UnityEngine;

public class CircleMissileSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.CircleMissileSpawner);
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			CircleMissileSpawnerComponent missile = e.circleMissileSpawner;
			missile.time -= deltaTime;
			if (missile.time < 0.0f) {
				missile.time = missile.spawnDelay;
				spawnMissiles(missile, e.position);
			}
		}
	}
	
	void spawnMissiles(CircleMissileSpawnerComponent missile, PositionComponent position) {
		float angleOffset = getAngle(missile.amount);
		float angle = 0.0f;
		float baseVelX = missile.velocityX;
		float baseVelY = missile.velocityY;

		for (int i = 0; i < missile.amount; i++) {
			float cosinus = Mathf.Cos(angle);
			float sinus = Mathf.Sin(angle);
			float velocityX = baseVelX * cosinus - baseVelY * sinus;
			float velocityY = baseVelX * sinus + baseVelY * cosinus;
			_pool.CreateEntity()
				.AddPosition(position.x, position.y)
				.AddVelocity(velocityX, velocityY)
				.AddHealth(0)
				.AddCollision(missile.collisionType)
				.AddResource(missile.resource);
			angle = angle + angleOffset;
		}
	}

	float getAngle(int amount) {
		return Mathf.Deg2Rad * (360.0f / (float)amount);
	}
}