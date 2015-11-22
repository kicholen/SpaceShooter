using Entitas;
using UnityEngine;

public class FirstBossSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;
	Group _player;

	const float EPSILON = 0.005f;

	bool tests = false;
	bool initalize = false;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.FirstBoss);
		_time = _pool.GetGroup(Matcher.Time);
		_player = pool.GetGroup(Matcher.Player);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _group.GetEntities()) {
			if (e.position.pos.y == 0.0f) {
				e.position.pos.y = 7.0f;
			}
			FirstBossComponent component = e.firstBoss;
			component.age += deltaTime;
			e.velocity.vel.Set(0.0f, 0.0f);
			//float age = component.age;
			//setVelocity(e.velocity, e.position, _player.GetSingleEntity().position);

			if (!tests) {
				if (!e.hasLaserSpawner) {
					e.AddLaserSpawner(5.0f, 0.0f, 0.0f, new UnityEngine.Vector2(), CollisionTypes.Enemy, null);
				}
				else {
					LaserSpawnerComponent laser = e.laserSpawner;
					laser.angle += component.laserAngle * deltaTime;

					if (laser.angle < EPSILON) {
						e.RemoveLaserSpawner();
					}
				}
				if (!initalize) {
					//e.AddCircleMissileRotatedSpawner(20, 8, 0, 10, 0.0f, 0.1f, Resource.MissileEnemy, 3.0f, CollisionTypes.Enemy);
					//e.AddCircleMissileSpawner(20, 2.0f, 0.05f, Resource.MissileEnemy, 4.0f, CollisionTypes.Enemy);
					//e.AddMultipleMissileSpawner(5, 5, 0.1f, 0.1f, 5.0f, 5.0f, Resource.MissileEnemy, 0.1f, velocity.x, -velocity.y, CollisionTypes.Enemy);
					initalize = true;
				}
			}
		}
	}

	void setVelocity(VelocityComponent velocity, PositionComponent actual, PositionComponent desired) {
		//velocity.x = (desired.x - actual.x) * 2.0f;
	}
}