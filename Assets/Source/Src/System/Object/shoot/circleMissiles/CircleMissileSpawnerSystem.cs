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
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			CircleMissileSpawnerComponent missile = e.circleMissileSpawner;
			missile.time -= deltaTime;
			if (missile.time < 0.0f) {
				missile.time = missile.spawnDelay;
				spawnMissiles(missile, e.position.pos);
			}
		}
	}
	
	void spawnMissiles(CircleMissileSpawnerComponent missile, Vector2 position) {
		float angleOffset = getAngle(missile.amount);
		float angle = 0.0f;
		float velocity = missile.velocity;

		for (int i = 0; i < missile.amount; i++) {
			float cosinus = Mathf.Cos(angle);
			float sinus = Mathf.Sin(angle);
			_pool.CreateEntity()
				.AddPosition(new Vector2().Set(position))
				.AddVelocity(new Vector2(velocity * cosinus - velocity * sinus, velocity * sinus + velocity * cosinus))
				.AddHealth(0)
				.AddCollision(missile.collisionType, missile.damage)
				.AddResource(missile.resource);
			angle = angle + angleOffset;
		}
	}

	float getAngle(int amount) {
		return Mathf.Deg2Rad * (360.0f / (float)amount);
	}
}