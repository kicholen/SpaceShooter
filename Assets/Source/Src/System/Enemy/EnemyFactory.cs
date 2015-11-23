using Entitas;
using System.Collections.Generic;
using UnityEngine;

	public class EnemyFactory {
	Pool _pool;
	Group _paths;

	public void SetPool(Pool pool, Group paths) {
		_pool = pool;
		_paths = paths;
		Random.seed = _pool.count;
	}

	public void CreateEnemyByType(int type, Vector2 position, int health, int missileSpeedBonus, int path, float speed = 5.0f) {
		Entity e;
		switch(type) {
		case EnemyTypes.Normal:
			e = createStandardEnemy(type, position, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, 0.0f, -4.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
		break;
		case EnemyTypes.Small:
			e = createStandardEnemy(type, position, health, speed, Resource.EnemySmall);
			e.isFaceDirection = true;
		break;
		case EnemyTypes.HomeMissile:
			e = createStandardEnemy(type, position, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddHomeMissileSpawner(4.0f, 2.0f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			break;
		case EnemyTypes.CircleMissile:
			e = createStandardEnemy(type, position, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileSpawner(5, 4.0f, 0.1f, Resource.MissileEnemy, 3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			break;
		case EnemyTypes.CircleRotateMissile:
			e = createStandardEnemy(type, position, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileRotatedSpawner(6, 4, 0, 10, 4.0f, 0.1f, Resource.MissileEnemy,  3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			break;
		case EnemyTypes.Meteor:
			e = createStandardEnemy(type, position, health, speed, Resource.Meteor);
			float randomAngle = Random.Range(-90.0f, 90.0f);
			e.AddRotate(randomAngle, randomAngle);
			break;
		case EnemyTypes.MovingBlockade:
			e = createStandardEnemy(type, position, health, speed, Resource.Blockade);
			e.AddMovingBlockade(2.0f, -1.0f, 0.0f, 2.0f, 1.0f);
			break;
		case EnemyTypes.FirstBoss:
			e = createFirstBoss(type, position, health, missileSpeedBonus);
			break;
		default:
			e = createStandardEnemy(type, position, health, speed, Resource.Enemy);
		break;
		}
		if (path > 0) {
			e.AddPath(0, position.y, 0.0f, _paths.GetEntities()[path - 1].pathModel);
		}
	}

	Entity createStandardEnemy(int type, Vector2 position, int health, float speed, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(position)
			.AddVelocity(new Vector2())
			.AddVelocityLimit(speed)
			.AddHealth(health)
			.AddCollision(CollisionTypes.Enemy)
			.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
			.AddExplosionOnDeath(1.0f, Resource.Explosion)
			.AddResource(resource);
		e.isNonRemovable = true;
		e.isActive = true;

		return e;
	}

	Entity createFirstBoss(int type, Vector2 position, int health, int missileSpeedBonus) {
		Entity boss = _pool.CreateEntity()
			.AddPosition(position)
			.AddVelocity(new Vector2())
			.AddVelocityLimit(5.0f)
			.AddCollision(CollisionTypes.Enemy)
			.AddHealth(health)
			.AddResource(Resource.Boss)
			.AddEnemy(type)
			.AddFirstBoss(22.0f, 0.0f, 90.0f);
		boss.isMoveWithCamera = true;
			

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
