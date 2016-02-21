using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory {
	Pool _pool;
	Group _paths;
    Group _enemies;
    Group _difficulty;

    DifficultyControllerComponent difficultyController;

    public void SetPool(Pool pool)
    {
        _pool = pool;
		_paths = pool.GetGroup(Matcher.PathModel);
		_enemies = pool.GetGroup(Matcher.EnemyModel);
        _difficulty = pool.GetGroup(Matcher.DifficultyController);
        Random.seed = _pool.count;
	}

	public Entity CreateEnemyByModel(EnemyModel model) {
        EnemyModelComponent component = getModelIfExist(model.type);
        return component != null ? createEnemyFromComponent(model, component) : createEnemyFromCode(model);
    }

    public Entity CreateEnemyByType(int type, float posX, float posY, int health, int path, int grid, int damage = 10, float speed = 5.0f)
    {
        EnemyModelComponent component = getModelIfExist(type);

        return component != null 
            ? createEnemyByTypeFromComponent(component, type, posX, posY, health, path, grid, damage, speed)
            : createEnemyByTypeFromCode(type, posX, posY, health, path, grid, damage, speed);
    }

    Entity createEnemyFromCode(EnemyModel model) {
        Entity e = null;
        float missileSpeedFactor = getMissileSpeedFactor();
        int damage = (int)(model.damage * getDamageFactor());
        
        switch (model.type) {
            case EnemyTypes.Normal:
                e = createStandardEnemy(model, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddMissileSpawner(0.0f, damage, 2.5f, ResourceWithColliders.MissileEnemy, new Vector2(0.0f, -4.0f * missileSpeedFactor), CollisionTypes.Enemy);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.Small:
                e = createStandardEnemy(model, ResourceWithColliders.EnemySmall);
                e.AddFaceDirection(true);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.HomeMissile:
                e = createStandardEnemy(model, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddHomeMissileSpawner(4.0f, 2.0f, damage, ResourceWithColliders.MissileEnemy, 2.0f * missileSpeedFactor, new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.CircleMissile:
                e = createStandardEnemy(model, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddCircleMissileSpawner(5, damage, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy, 3.0f * missileSpeedFactor, CollisionTypes.Enemy);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.CircleRotateMissile:
                e = createStandardEnemy(model, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddCircleMissileRotatedSpawner(6, damage, 4, 0, 10, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy, 3.0f * missileSpeedFactor, CollisionTypes.Enemy);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.TargetMissile:
                e = createStandardEnemy(model, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddTargetMissileSpawner(6, damage, 6, ResourceWithColliders.MissileEnemy, 3.0f * missileSpeedFactor, CollisionTypes.Enemy, CollisionTypes.Player);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.Meteor:
                e = createStandardEnemy(model, ResourceWithColliders.Enemy);
                float randomAngle = Random.Range(-90.0f, 90.0f);
                e.AddRotate(randomAngle, randomAngle);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.MovingBlock:
                e = createStandardEnemy(model, ResourceWithColliders.Blockade);
                e.AddMovingBlockade(2.0f, -1.0f, 0.0f, model.speed, model.speed / 2.0f);
                addPathIfNeeded(e, model.posY, model.path);
                break;
            case EnemyTypes.MovingBlockade:
                for (int i = 10; i > 0; i--) {
                    if (i == 5) {
                        e = createBlock(model.type, damage, new Vector2(-4.0f + (float)i, model.posY), 1, ResourceWithColliders.Meteor);
                    }
                    else {
                        e = createBlock(model.type, damage, new Vector2(-4.0f + (float)i, model.posY), model.health, ResourceWithColliders.Blockade);
                    }
                    e.AddMovingBlockade(2.0f, -1.0f, 0.0f, model.speed, model.speed / 2.0f);
                }
                break;
            case EnemyTypes.FirstBoss:
                e = createFirstBoss(model.type, model.posX, model.posY, model.health, missileSpeedFactor);
                break;
            default:
                e = createStandardEnemy(model, ResourceWithColliders.Enemy);
                break;
        }
        return e;
    }

    Entity createEnemyByTypeFromComponent(EnemyModelComponent component, int type, float posX, float posY, int health, int path, int grid, int damage, float velocityLimit) {
        Entity e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, component.resource);
        e.AddFaceDirection(true);
        e.AddCameraShakeOnDeath(1);
        addPathIfNeeded(e, posY, path);
        addWeapon(e, damage, component);
        return e;
    }

    Entity createEnemyByTypeFromCode(int type, float posX, float posY, int health, int path, int grid, int damage, float velocityLimit) {
        Entity e = null;
        float missileSpeedFactor = getMissileSpeedFactor();
        damage = (int)(damage * getDamageFactor());

        switch (type) {
            case EnemyTypes.Normal:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddMissileSpawner(0.0f, damage, 2.5f, ResourceWithColliders.MissileEnemy, new Vector2(0.0f, -4.0f * missileSpeedFactor), CollisionTypes.Enemy);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.Small:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.EnemySmall);
                e.AddFaceDirection(true);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.HomeMissile:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddHomeMissileSpawner(4.0f, 2.0f, damage, ResourceWithColliders.MissileEnemy, 2.0f * missileSpeedFactor, new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.CircleMissile:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddCircleMissileSpawner(5, damage, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy, 3.0f * missileSpeedFactor, CollisionTypes.Enemy);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.CircleRotateMissile:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddCircleMissileRotatedSpawner(6, damage, 4, 0, 10, 4.0f, 0.1f, ResourceWithColliders.MissileEnemy, 3.0f * missileSpeedFactor, CollisionTypes.Enemy);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.TargetMissile:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Enemy);
                e.AddFaceDirection(true);
                e.AddCameraShakeOnDeath(1);
                e.AddTargetMissileSpawner(6, damage, 6, ResourceWithColliders.MissileEnemy, 3.0f * missileSpeedFactor, CollisionTypes.Enemy, CollisionTypes.Player);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.Meteor:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Meteor);
                float randomAngle = Random.Range(-90.0f, 90.0f);
                e.AddRotate(randomAngle, randomAngle);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.MovingBlock:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Blockade);
                e.AddMovingBlockade(2.0f, -1.0f, 0.0f, velocityLimit, velocityLimit / 2.0f);
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
                    e.AddMovingBlockade(2.0f, -1.0f, 0.0f, velocityLimit, velocityLimit / 2.0f);
                }
                break;
            case EnemyTypes.FirstBoss:
                e = createFirstBoss(type, posX, posY, health, missileSpeedFactor);
                break;
            default:
                e = createStandardEnemy(type, damage, posX, posY, health, velocityLimit * missileSpeedFactor, ResourceWithColliders.Enemy);
                break;
        }
        return e;
    }

    Entity createEnemyFromComponent(EnemyModel model, EnemyModelComponent component) {
        Entity e = createStandardEnemy(model, component.resource);
        e.AddFaceDirection(true);
        e.AddCameraShakeOnDeath(1);
        addPathIfNeeded(e, model.posY, model.path);
        addWeapon(e, model.damage, component);
        return e;
    }

    void addWeapon(Entity e, int damage, EnemyModelComponent component) {
        damage = (int)(damage * getDamageFactor());

        switch (component.weapon) {
            case WeaponTypes.Circle:
                e.AddCircleMissileSpawner(component.amount, damage, component.time, component.spawnDelay, component.weaponResource,
                    component.velocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.CircleRotated:
                e.AddCircleMissileRotatedSpawner(component.amount, damage, component.waves, component.angle, component.angleOffset,
                    component.time, component.spawnDelay, component.weaponResource, component.velocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Dispersion:
                e.AddDispersionMissileSpawner(component.time, damage, component.spawnDelay, component.angle, component.weaponResource,
                    component.velocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Home:
                e.AddHomeMissileSpawner(component.time, component.spawnDelay, damage, component.weaponResource, component.velocity * getMissileSpeedFactor(),
                    component.startVelocity, component.followDelay, component.selfDestructionDelay, CollisionTypes.Enemy);
                break;
            case WeaponTypes.Laser:
                e.AddLaserSpawner(0.0f, component.velocity, component.velocity, component.angle, Vector2.up, CollisionTypes.Enemy, damage, Resource.Laser, null);
                break;
            case WeaponTypes.Multiple:
                e.AddMultipleMissileSpawner(component.amount, damage, 0, component.timeDelay, component.delay, component.time,
                    component.spawnDelay, component.weaponResource, component.randomPositionOffsetX, component.startVelocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Single:
                e.AddMissileSpawner(component.time, damage, component.spawnDelay, component.weaponResource, component.startVelocity * getMissileSpeedFactor(), CollisionTypes.Enemy);
                break;
            case WeaponTypes.Target:
                e.AddTargetMissileSpawner(component.time, damage, component.spawnDelay, component.weaponResource, component.velocity * getMissileSpeedFactor(),
                    CollisionTypes.Enemy, CollisionTypes.Player);
                break;
        }
    }

    Entity createStandardEnemy(EnemyModel model, string resource) {
        float healthFactor = getHealthFactor();

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
            e.AddHealth((int)(model.health * healthFactor))
                .AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed | BonusTypes.Laser | BonusTypes.Shield | BonusTypes.Atom | BonusTypes.FireRate | BonusTypes.Damage)
                .AddExplosionOnDeath(1.0f, Resource.Explosion);
		}
		return e;
	}

	Entity createStandardEnemy(int type, int damage, float posX, float posY, int health, float speedLimit, string resource) {
		Entity e = _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(new Vector2(posX, posY))
			.AddVelocity(new Vector2())
			.AddVelocityLimit(speedLimit)
			.AddResource(resource)
			.AddCollision(CollisionTypes.Enemy, damage)
			.IsNonRemovable(true)
			.IsActive(true);
		if (health >= 0) {
			e.AddHealth(health)
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed | BonusTypes.Laser | BonusTypes.Shield | BonusTypes.Atom | BonusTypes.FireRate | BonusTypes.Damage)
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
				.AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed | BonusTypes.Laser | BonusTypes.Shield | BonusTypes.Atom | BonusTypes.FireRate | BonusTypes.Damage)
                .AddExplosionOnDeath(1.0f, Resource.Explosion);
		}
		return e;
	}

	Entity createFirstBoss(int type, float posX, float posY, int health, float missileSpeedFactor) {
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
		             .AddHomeMissileSpawner(5.0f, 10f, 10, ResourceWithColliders.MissileEnemyHoming, 2.0f * missileSpeedFactor,
                        new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy)
		             .AddResource(Resource.Weapon));
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(-0.5f, 0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(boss)
		             .AddHomeMissileSpawner(5.0f, 10f, 10, ResourceWithColliders.MissileEnemyHoming, 2.0f * missileSpeedFactor,
                        new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy)
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

    EnemyModelComponent getModelIfExist(int type) {
        return _enemies.count >= type && type != 0 ? _enemies.GetEntities()[type - 1].enemyModel : null;
    }

    float getHealthFactor()
    {
        return (getDifficultyController().hpBoostPercent + 100.0f) / 100.0f;
    }

    float getMissileSpeedFactor()
    {
        return (getDifficultyController().missileSpeedBoostPercent + 100.0f) / 100.0f;
    }

    float getDamageFactor()
    {
        return (getDifficultyController().dmgBoostPercent + 100.0f) / 100.0f;
    }

    DifficultyControllerComponent getDifficultyController()
    {
        if (difficultyController == null)
            difficultyController = _difficulty.GetSingleEntity().difficultyController;
        return difficultyController;
    }
}
