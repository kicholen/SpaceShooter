using Entitas;
using System.Collections.Generic;

public class EnemyFactory {
	Pool _pool;
	Group _players;

	public void SetPool(Pool pool) {
		_pool = pool;
		_players = pool.GetGroup(Matcher.Player);
	}
	
	public void createEnemyByType(int type, float x, float y, float velocityX, float velocityY, int health) {
		Entity e;
		switch(type) {
		case 0:
			e = createStandardEnemy(type, x, y, velocityX, velocityY, health);
			e.isFaceDirection = true;
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, 0.0f, -4.0f, CollisionTypes.Enemy);
		break;
		case 1:
			e = createStandardEnemy(type, x, y, velocityX, velocityY, health);
			e.isFaceDirection = true;
			e.AddHomeMissileSpawner(0.0f, 2.0f, Resource.MissileEnemy, 2.0f, CollisionTypes.Enemy);
		break;
		case 101:
			e = createFirstBoss(type, x, y, velocityX, velocityY, health);
			break;
		default:
			e = createStandardEnemy(type, x, y, velocityX, velocityY, health);
		break;
		}
	}

	Entity createStandardEnemy(int type, float x, float y, float velocityX, float velocityY, int health) {
		return _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(x, y)
			.AddVelocity(velocityX, velocityY)
			.AddVelocityLimit(5.0f, 5.0f, 0.0f, 0.0f)
			.AddHealth(health)
			.AddCollision(CollisionTypes.Enemy)
			.AddBonusOnDeath(1)
			.AddCameraShakeOnDeath(1)
			.AddParticlesOnDeath(1)
			.AddResource(Resource.Enemy);
	}

	Entity createFirstBoss(int type, float x, float y, float velocityX, float velocityY, int health) {
		Entity boss = _pool.CreateEntity()
			.AddPosition(x, y)
			.AddVelocity(velocityX, velocityY)
			.AddVelocityLimit(5.0f, 5.0f, 0.0f, 0.0f)
			.AddCollision(CollisionTypes.Enemy)
			.AddHealth(health)
			.AddResource(Resource.Boss)
			.AddEnemy(type)
			.AddFirstBoss(22.0f, 0.0f, 90.0f);

		List<Entity> children = new List<Entity>();
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(0.5f, 0.5f)
		             .AddPosition(0.0f, 0.0f)
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, Resource.MissileEnemy, 2.0f, CollisionTypes.Enemy)
		             .AddResource(Resource.Weapon));
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(-0.5f, 0.5f)
		             .AddPosition(0.0f, 0.0f)
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, Resource.MissileEnemy, 2.0f, CollisionTypes.Enemy)
		             .AddResource(Resource.Weapon));
		addNonRemovable(children);
		boss.AddParent(children);
		return boss;
	}

	void addNonRemovable(List<Entity> entities) {
		foreach (Entity e in entities) {
			e.isNonRemovable = true;
		}
	}
}
