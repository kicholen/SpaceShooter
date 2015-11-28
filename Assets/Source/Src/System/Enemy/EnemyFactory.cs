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

	public void CreateEnemyByModel(EnemyModel model, int missileSpeedBonus, float healthMultiplier) {
		Entity e;
		switch(model.type) {
		case EnemyTypes.Normal:
			e = createStandardEnemy(model, healthMultiplier, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, 0.0f, -4.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, model.posY, model.path);
			break;
		case EnemyTypes.Small:
			e = createStandardEnemy(model, healthMultiplier, Resource.EnemySmall);
			e.isFaceDirection = true;
			addPath(e, model.posY, model.path);
			break;
		case EnemyTypes.HomeMissile:
			e = createStandardEnemy(model, healthMultiplier, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddHomeMissileSpawner(4.0f, 2.0f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, model.posY, model.path);
			break;
		case EnemyTypes.CircleMissile:
			e = createStandardEnemy(model, healthMultiplier, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileSpawner(5, 4.0f, 0.1f, Resource.MissileEnemy, 3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, model.posY, model.path);
			break;
		case EnemyTypes.CircleRotateMissile:
			e = createStandardEnemy(model, healthMultiplier, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileRotatedSpawner(6, 4, 0, 10, 4.0f, 0.1f, Resource.MissileEnemy,  3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, model.posY, model.path);
			break;
		case EnemyTypes.Meteor:
			e = createStandardEnemy(model, healthMultiplier, Resource.Enemy);
			float randomAngle = Random.Range(-90.0f, 90.0f);
			e.AddRotate(randomAngle, randomAngle);
			addPath(e, model.posY, model.path);
			break;
		case EnemyTypes.MovingBlock:
			e = createStandardEnemy(model, healthMultiplier, Resource.Blockade);
			e.AddMovingBlockade(2.0f, -1.0f, 0.0f, model.speed, model.speed / 2.0f);
			addPath(e, model.posY, model.path);
			break;
		case EnemyTypes.MovingBlockade:
			for (int i = 10; i > 0; i--) {
				if (i == 5) {
					e = createBlock(model.type, new Vector2(-4.0f + (float)i, model.posY), 1, Resource.Meteor);
				}
				else {
					e = createBlock(model.type, new Vector2(-4.0f + (float)i, model.posY), model.health, Resource.Blockade);
				}
				e.AddMovingBlockade(2.0f, -1.0f, 0.0f, model.speed, model.speed / 2.0f);
			}
			break;
		case EnemyTypes.FirstBoss:
			e = createFirstBoss(model.type, model.posX, model.posY, model.health, missileSpeedBonus);
			break;
		default:
			e = createStandardEnemy(model, healthMultiplier, Resource.Enemy);
			break;
		}
	}

	public void CreateEnemyByType(int type, float posX, float posY, int health, int missileSpeedBonus, int path, float speed = 5.0f) {
		Entity e;
		switch(type) {
		case EnemyTypes.Normal:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, 0.0f, -4.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, posY, path);
		break;
		case EnemyTypes.Small:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.EnemySmall);
			e.isFaceDirection = true;
			addPath(e, posY, path);
		break;
		case EnemyTypes.HomeMissile:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddHomeMissileSpawner(4.0f, 2.0f, Resource.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, posY, path);
			break;
		case EnemyTypes.CircleMissile:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileSpawner(5, 4.0f, 0.1f, Resource.MissileEnemy, 3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, posY, path);
			break;
		case EnemyTypes.CircleRotateMissile:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.Enemy);
			e.isFaceDirection = true;
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileRotatedSpawner(6, 4, 0, 10, 4.0f, 0.1f, Resource.MissileEnemy,  3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPath(e, posY, path);
			break;
		case EnemyTypes.Meteor:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.Meteor);
			float randomAngle = Random.Range(-90.0f, 90.0f);
			e.AddRotate(randomAngle, randomAngle);
			addPath(e, posY, path);
			break;
		case EnemyTypes.MovingBlock:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.Blockade);
			e.AddMovingBlockade(2.0f, -1.0f, 0.0f, speed, speed / 2.0f);
			addPath(e, posY, path);
			break;
		case EnemyTypes.MovingBlockade:
			for (int i = 10; i > 0; i--) {
				if (i == 5) {
					e = createBlock(type, new Vector2(-4.0f + (float)i, posY), 1, Resource.Meteor);
				}
				else {
					e = createBlock(type, new Vector2(-4.0f + (float)i, posY), health, Resource.Blockade);
				}
				e.AddMovingBlockade(2.0f, -1.0f, 0.0f, speed, speed / 2.0f);
			}
			break;
		case EnemyTypes.FirstBoss:
			e = createFirstBoss(type, posX, posY, health, missileSpeedBonus);
			break;
		default:
			e = createStandardEnemy(type, posX, posY, health, speed, Resource.Enemy);
		break;
		}
	}

	Entity createStandardEnemy(EnemyModel model, float healthMultiplier, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(model.type)
			.AddPosition(new Vector2(model.posX, model.posY))
			.AddVelocity(new Vector2())
			.AddVelocityLimit(model.speed)
			.AddResource(resource);
		e.isNonRemovable = true;
		e.isActive = true;
		if (model.health >= 0) {
			e.AddHealth((int)(model.health * healthMultiplier))
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
				.AddExplosionOnDeath(1.0f, Resource.Explosion)
				.AddCollision(CollisionTypes.Enemy);
		}
		return e;
	}

	Entity createStandardEnemy(int type, float posX, float posY, int health, float speed, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(new Vector2(posX, posY))
			.AddVelocity(new Vector2())
			.AddVelocityLimit(speed)
			.AddResource(resource);
		e.isNonRemovable = true;
		e.isActive = true;
		if (health >= 0) {
			e.AddHealth(health)
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
				.AddExplosionOnDeath(1.0f, Resource.Explosion)
				.AddCollision(CollisionTypes.Enemy);
		}
		return e;
	}

	Entity createBlock(int type, Vector2 position, int health, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(position)
			.AddResource(resource);
		if (health >= 0) {
			e.AddHealth(health)
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
				.AddExplosionOnDeath(1.0f, Resource.Explosion)
				.AddCollision(CollisionTypes.Enemy);
		}
		return e;
	}

	Entity createFirstBoss(int type, float posX, float posY, int health, int missileSpeedBonus) {
		Entity boss = _pool.CreateEntity()
			.AddPosition(new Vector2(posX, posY))
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

	void addPath(Entity e, float offsetY, int path) {
		if (path > 0) {
			e.AddPath(0, offsetY, 0.0f, _paths.GetEntities()[path - 1].pathModel);
		}
	}
}
