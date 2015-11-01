using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory {
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void createEnemyByType(int type, Vector2 position, Vector2 velocity, int health, int missileSpeedBonus) {
		Entity e;
		switch(type) {
		case 0:
			e = createStandardEnemy(type, position, velocity, health);
			e.isFaceDirection = true;
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, 0.0f, -4.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
		break;
		case 1:
			e = createStandardEnemy(type, position, velocity, health);
			e.isFaceDirection = true;
			e.AddHomeMissileSpawner(0.0f, 2.0f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
		break;
		case 101:
			e = createFirstBoss(type, position, velocity, health, missileSpeedBonus);
			break;
		default:
			e = createStandardEnemy(type, position, velocity, health);
		break;
		}
	}

	Entity createStandardEnemy(int type, Vector2 position, Vector2 velocity, int health) {
		return _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(new Vector2().Set(position))
			.AddVelocity(new Vector2().Set(velocity))
			.AddVelocityLimit(5.0f)
			.AddHealth(health)
			.AddCollision(CollisionTypes.Enemy)
			.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
			.AddCameraShakeOnDeath(1)
			.AddParticlesOnDeath(1)
			.AddResource(Resource.Enemy);
	}

	Entity createFirstBoss(int type, Vector2 position, Vector2 velocity, int health, int missileSpeedBonus) {
		Entity boss = _pool.CreateEntity()
			.AddPosition(new Vector2().Set(position))
			.AddVelocity(new Vector2().Set(velocity))
			.AddVelocityLimit(5.0f)
			.AddCollision(CollisionTypes.Enemy)
			.AddHealth(health)
			.AddResource(Resource.Boss)
			.AddEnemy(type)
			.AddFirstBoss(22.0f, 0.0f, 90.0f);

		List<Entity> children = new List<Entity>();
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(0.5f, 0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy)
		             .AddResource(Resource.Weapon));
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(-0.5f, 0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy)
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
