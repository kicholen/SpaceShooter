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
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Enemy);
            e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddMissileSpawner(0.0f, model.damage, 2.5f, ResourceWithColliders.MissileEnemy, new Vector2(0.0f, -4.0f * (missileSpeedBonus + 100) / 100), CollisionTypes.Enemy);
			addPathIfNeeded(e, model.posY, model.path);
			break;
		case EnemyTypes.Small:
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.EnemySmall);
			e.AddFaceDirection(true);
			addPathIfNeeded(e, model.posY, model.path);
			break;
		case EnemyTypes.HomeMissile:
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Enemy);
			e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddHomeMissileSpawner(4.0f, 2.0f, model.damage, ResourceWithColliders.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy);
			addPathIfNeeded(e, model.posY, model.path);
			break;
		case EnemyTypes.CircleMissile:
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Enemy);
			e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileSpawner(5, model.damage, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy, 3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPathIfNeeded(e, model.posY, model.path);
			break;
		case EnemyTypes.CircleRotateMissile:
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Enemy);
			e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileRotatedSpawner(6, model.damage, 4, 0, 10, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy,  3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPathIfNeeded(e, model.posY, model.path);
			break;
        case EnemyTypes.TargetMissile:
            e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Enemy);
            e.AddFaceDirection(true);
            e.AddCameraShakeOnDeath(1);
            e.AddTargetMissileSpawner(6, model.damage, 6, ResourceWithColliders.MissileEnemy, 3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy, CollisionTypes.Player);
            addPathIfNeeded(e, model.posY, model.path);
            break;
        case EnemyTypes.Meteor:
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Enemy);
			float randomAngle = Random.Range(-90.0f, 90.0f);
			e.AddRotate(randomAngle, randomAngle);
			addPathIfNeeded(e, model.posY, model.path);
			break;
		case EnemyTypes.MovingBlock:
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Blockade);
			e.AddMovingBlockade(2.0f, -1.0f, 0.0f, model.speed, model.speed / 2.0f);
			addPathIfNeeded(e, model.posY, model.path);
			break;
		case EnemyTypes.MovingBlockade:
			for (int i = 10; i > 0; i--) {
				if (i == 5) {
					e = createBlock(model.type, model.damage, new Vector2(-4.0f + (float)i, model.posY), 1, ResourceWithColliders.Meteor);
				}
				else {
					e = createBlock(model.type, model.damage, new Vector2(-4.0f + (float)i, model.posY), model.health, ResourceWithColliders.Blockade);
				}
				e.AddMovingBlockade(2.0f, -1.0f, 0.0f, model.speed, model.speed / 2.0f);
			}
			break;
		case EnemyTypes.FirstBoss:
			e = createFirstBoss(model.type, model.posX, model.posY, model.health, missileSpeedBonus);
			break;
		default:
			e = createStandardEnemy(model, healthMultiplier, ResourceWithColliders.Enemy);
			break;
		}
	}

	public void CreateEnemyByType(int type, float posX, float posY, int health, int missileSpeedBonus, int path, int grid, int damage = 10, float speed = 5.0f) {
		Entity e;
		switch(type) {
		case EnemyTypes.Normal:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Enemy);
			e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddMissileSpawner(0.0f, damage, 2.5f, ResourceWithColliders.MissileEnemy, new Vector2(0.0f, -4.0f * (missileSpeedBonus + 100) / 100), CollisionTypes.Enemy);
			addPathIfNeeded(e, posY, path);
			addGridIfNeeded(e, grid);
		break;
		case EnemyTypes.Small:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.EnemySmall);
			e.AddFaceDirection(true);
			addPathIfNeeded(e, posY, path);
			addGridIfNeeded(e, grid);
		break;
		case EnemyTypes.HomeMissile:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Enemy);
			e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddHomeMissileSpawner(4.0f, 2.0f, damage, ResourceWithColliders.MissileEnemy, 2.0f * (missileSpeedBonus + 100) / 100, new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy);
			addPathIfNeeded(e, posY, path);
			addGridIfNeeded(e, grid);
			break;
		case EnemyTypes.CircleMissile:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Enemy);
			e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileSpawner(5, damage, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy, 3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPathIfNeeded(e, posY, path);
			addGridIfNeeded(e, grid);
			break;
		case EnemyTypes.CircleRotateMissile:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Enemy);
			e.AddFaceDirection(true);
			e.AddCameraShakeOnDeath(1);
			e.AddCircleMissileRotatedSpawner(6, damage, 4, 0, 10, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy,  3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy);
			addPathIfNeeded(e, posY, path);
			addGridIfNeeded(e, grid);
			break;
        case EnemyTypes.TargetMissile:
            e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Enemy);
            e.AddFaceDirection(true);
            e.AddCameraShakeOnDeath(1);
            e.AddTargetMissileSpawner(6, damage, 6, ResourceWithColliders.MissileEnemy, 3.0f * (missileSpeedBonus + 100) / 100, CollisionTypes.Enemy, CollisionTypes.Player);
            addPathIfNeeded(e, posY, path);
            addGridIfNeeded(e, grid);
            break;
        case EnemyTypes.Meteor:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Meteor);
			float randomAngle = Random.Range(-90.0f, 90.0f);
			e.AddRotate(randomAngle, randomAngle);
			addPathIfNeeded(e, posY, path);
			addGridIfNeeded(e, grid);
			break;
		case EnemyTypes.MovingBlock:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Blockade);
			e.AddMovingBlockade(2.0f, -1.0f, 0.0f, speed, speed / 2.0f);
			addPathIfNeeded(e, posY, path);
			addGridIfNeeded(e, grid);
			break;
		case EnemyTypes.MovingBlockade:
			for (int i = 10; i > 0; i--) {
				if (i == 5) {
					e = createBlock(type, damage, new Vector2(-4.0f + (float)i, posY), 1, ResourceWithColliders.Meteor);
				}
				else {
					e = createBlock(type, damage, new Vector2(-4.0f + (float)i, posY), health, ResourceWithColliders.Blockade);
				}
				e.AddMovingBlockade(2.0f, -1.0f, 0.0f, speed, speed / 2.0f);
			}
			break;
		case EnemyTypes.FirstBoss:
			e = createFirstBoss(type, posX, posY, health, missileSpeedBonus);
			break;
		default:
			e = createStandardEnemy(type, damage, posX, posY, health, speed, ResourceWithColliders.Enemy);
		break;
		}
	}

	Entity createStandardEnemy(EnemyModel model, float healthMultiplier, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(model.type)
			.AddPosition(new Vector2(model.posX, model.posY))
			.AddVelocity(new Vector2())
			.AddVelocityLimit(model.speed)
			.AddResource(resource)
			.AddCollision(CollisionTypes.Enemy, model.damage)
			.IsNonRemovable(true)
			.IsActive(true);
		if (model.health >= 0) {
			e.AddHealth((int)(model.health * healthMultiplier))
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
				.AddExplosionOnDeath(1.0f, Resource.Explosion);
		}
		return e;
	}

	Entity createStandardEnemy(int type, int damage, float posX, float posY, int health, float speed, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(new Vector2(posX, posY))
			.AddVelocity(new Vector2())
			.AddVelocityLimit(speed)
			.AddResource(resource)
			.AddCollision(CollisionTypes.Enemy, damage)
			.IsNonRemovable(true)
			.IsActive(true);
		if (health >= 0) {
			e.AddHealth(health)
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
				.AddExplosionOnDeath(1.0f, Resource.Explosion);
		}
		return e;
	}

	Entity createBlock(int type, int damage, Vector2 position, int health, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(position)
			.AddResource(resource)
			.AddCollision(CollisionTypes.Enemy, damage);
		if (health >= 0) {
			e.AddHealth(health)
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
				.AddExplosionOnDeath(1.0f, Resource.Explosion);
		}
		return e;
	}

	Entity createFirstBoss(int type, float posX, float posY, int health, int missileSpeedBonus) {
		Entity boss = _pool.CreateEntity()
			.AddPosition(new Vector2(posX, posY))
			.AddVelocity(new Vector2())
			.AddVelocityLimit(5.0f)
			.AddCollision(CollisionTypes.Enemy, health)
			.AddHealth(health)
			.AddResource(ResourceWithColliders.Boss)
			.AddEnemy(type)
			.AddFirstBoss(22.0f, 0.0f, 90.0f);
		boss.isMoveWithCamera = true;
			

		List<Entity> children = new List<Entity>();
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(0.5f, 0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, 10, ResourceWithColliders.MissileEnemyHoming, 2.0f * (missileSpeedBonus + 100) / 100, new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy)
		             .AddResource(Resource.Weapon));
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(-0.5f, 0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, 10, ResourceWithColliders.MissileEnemyHoming, 2.0f * (missileSpeedBonus + 100) / 100, new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy)
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

	void addPathIfNeeded(Entity e, float offsetY, int path) {
		if (path > 0) {
			e.AddPath(0, offsetY, 0.0f, _paths.GetEntities()[path - 1].pathModel);
		}
	}

	void addGridIfNeeded(Entity e, int grid) {
		if (grid > 0) {
			e.AddGridField(0.0f, 1.0f, GridFieldState.INACTIVE, grid, -1, -1);
		}
	}
}
