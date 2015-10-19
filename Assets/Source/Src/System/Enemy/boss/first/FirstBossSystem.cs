using Entitas;

public class FirstBossSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;

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
			if (!tests) {
				tests = true;
				VelocityComponent velocity = e.velocity;
				//e.AddLaserSpawner(5.0f, 0.0f, 180, CollisionTypes.Enemy, null)
				e.AddCircleMissileRotatedSpawner(4, 20, 0, 10, 0.0f, 0.1f, Resource.MissileEnemy, velocity.x, velocity.y, velocity.x, velocity.y, CollisionTypes.Enemy);
				e.AddMultipleMissileSpawner(5, 5, 0.1f, 0.1f, 5.0f, 5.0f, Resource.MissileEnemy, 0.1f, velocity.x, -velocity.y, CollisionTypes.Enemy);
			}
		}
	}
}