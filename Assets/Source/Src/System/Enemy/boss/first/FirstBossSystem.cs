using Entitas;

public class FirstBossSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;

	const float EPSILON = 0.005f;

	bool tests = false;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.FirstBoss);
		_time = _pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;

		foreach (Entity e in _group.GetEntities()) {
			FirstBossComponent component = e.firstBoss;
			component.age += deltaTime;
			float age = component.age;

			if (!tests) {
				VelocityComponent velocity = e.velocity;
				if (!e.hasLaserSpawner) {
					e.AddLaserSpawner(5.0f, 0.0f, 0.0f, new UnityEngine.Vector2(), CollisionTypes.Enemy, null);
				}
				else {
					LaserSpawnerComponent laser = e.laserSpawner;
					laser.angle += component.laserAngle;
					if (System.Math.Abs(laser.angle - 0.0f) < EPSILON) {
						e.RemoveLaserSpawner ();
					}
				}
				//e.AddCircleMissileRotatedSpawner(4, 20, 0, 10, 0.0f, 0.1f, Resource.MissileEnemy, velocity.x, velocity.y, velocity.x, velocity.y, CollisionTypes.Enemy);
				//e.AddMultipleMissileSpawner(5, 5, 0.1f, 0.1f, 5.0f, 5.0f, Resource.MissileEnemy, 0.1f, velocity.x, -velocity.y, CollisionTypes.Enemy);
			}
		}
	}
}