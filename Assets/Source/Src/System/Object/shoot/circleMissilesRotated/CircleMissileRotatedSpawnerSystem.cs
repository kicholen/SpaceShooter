using Entitas;
using UnityEngine;

/**
 * Spawner is removed on finish.
 */
public class CircleMissileRotatedSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.CircleMissileRotatedSpawner);
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			CircleMissileRotatedSpawnerComponent component = e.circleMissileRotatedSpawner;
			component.time -= deltaTime;
			if (component.time < 0.0f) {
				component.time = component.spawnDelay;
				spawnMissiles(component, e.position);
				component.angle += component.angleOffset;
				component.waves--;
				if (component.waves == 0) {
					e.RemoveCircleMissileRotatedSpawner();
				}
			}
		}
	}
	
	void spawnMissiles(CircleMissileRotatedSpawnerComponent missile, PositionComponent position) {
		float angleOffset = getAngle(missile.amount);
		float angle = missile.angle;
		float baseVelX = missile.velocityX;
		float baseVelY = missile.velocityY;

		for (int i = 0; i < missile.amount; i++) {
			float cosinus = Mathf.Cos(angle);
			float sinus = Mathf.Sin(angle);
			float velocityX = baseVelX * cosinus - baseVelY * sinus;
			float velocityY = baseVelX * sinus + baseVelY * cosinus;
			_pool.CreateEntity()
				.AddPosition(position.x, position.y)
				.AddVelocity(velocityX + missile.velocityOffsetX, velocityY + missile.velocityOffsetY)
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