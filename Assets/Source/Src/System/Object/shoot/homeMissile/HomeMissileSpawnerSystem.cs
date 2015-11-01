using Entitas;

public class HomeMissileSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;
	Group _enemies;
	Group _player;

	const float SELF_DESTRUCTION_TIME = 3.0f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.HomeMissileSpawner);
		_time = pool.GetGroup(Matcher.Time);
		_enemies = pool.GetGroup(Matcher.Enemy);
		_player = pool.GetGroup(Matcher.Player);
	}
	
	public void Execute() {
		bool canSpawnPlayer = canSpawnPlayerMissile();
		bool canSpawnEnemy = canSpawnEnemyMissile();
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			HomeMissileSpawnerComponent missile = e.homeMissileSpawner;
			missile.time -= deltaTime;
			if (missile.time < 0.0f) {
				missile.time = missile.spawnDelay;
				if (missile.collisionType == CollisionTypes.Player && canSpawnPlayer) {
					spawnMissile(missile, e.position);
				}
				else if (missile.collisionType == CollisionTypes.Enemy && canSpawnEnemy) {
					spawnMissile(missile, e.position);
				}
			}
		}
	}
	
	void spawnMissile(HomeMissileSpawnerComponent missile, PositionComponent position) {
		_pool.CreateEntity()
			.AddPosition(position.x, position.y)
			.AddVelocity(0.0f, 0.0f)
			.AddVelocityLimit(missile.velocity, missile.velocity, 0.0f, 0.0f)
			.AddHealth(0)
			.AddHomeMissile(0.0f)
			.AddFindTarget(missile.collisionType)
			.AddCollision(missile.collisionType)
			.AddDestroyEntityDelayed(SELF_DESTRUCTION_TIME)
			.AddResource(missile.resource);
	}

	bool canSpawnPlayerMissile() {
		return _enemies.count > 0;
	}

	bool canSpawnEnemyMissile() {
		return _player.count > 0;
	}
}