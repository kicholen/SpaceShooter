using Entitas;
using UnityEngine;

public class HomeMissileSpawnerSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;
	Group _enemies;
	Group _player;

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
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			HomeMissileSpawnerComponent missile = e.homeMissileSpawner;
			missile.time -= deltaTime;
			if (missile.time < 0.0f) {
				missile.time = missile.spawnDelay;
				if (missile.ownerCollisionType == CollisionTypes.Player && canSpawnPlayer) {
					spawnMissile(missile, CollisionTypes.Enemy, e.position.pos);
				}
				else if (missile.ownerCollisionType == CollisionTypes.Enemy && canSpawnEnemy) {
					spawnMissile(missile, CollisionTypes.Player, e.position.pos);
				}
			}
		}
	}
	
	void spawnMissile(HomeMissileSpawnerComponent missile, int targetCollisionType, Vector2 position) {
		_pool.CreateEntity()
			.AddPosition(new Vector2(position.x, position.y))
			.AddVelocity(missile.startVelocity)
			.AddHealth(0)
			.AddHomeMissile(missile.followDelay, missile.velocity, targetCollisionType)
			.AddFindTarget(targetCollisionType)
			.AddCollision(missile.ownerCollisionType, missile.damage)
			.AddDestroyEntityDelayed(missile.selfDestructionDelay)
			.AddFaceDirection(true)
            .AddExplosionOnDeath(1.5f, Resource.ExplosionMissile)
            .AddResource(missile.resource);
	}

	bool canSpawnPlayerMissile() {
		return _enemies.count > 0;
	}

	bool canSpawnEnemyMissile() {
		return _player.count > 0;
	}
}