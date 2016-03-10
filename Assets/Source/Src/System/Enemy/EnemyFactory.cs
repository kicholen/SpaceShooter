using Entitas;
using UnityEngine;

public class EnemyFactory {
	Group paths;
    Group enemies;
    Group difficulty;

    DifficultyControllerComponent difficultyController;
    EnemyCreator enemyCreator;
    BossCreator bossCreator;
    WeaponProvider weaponProvider;

    public void SetPool(Pool pool)
    {
		paths = pool.GetGroup(Matcher.PathModel);
		enemies = pool.GetGroup(Matcher.EnemyModel);
        difficulty = pool.GetGroup(Matcher.DifficultyController);
        enemyCreator = new EnemyCreator(pool);
        bossCreator = new BossCreator(pool);
        weaponProvider = new WeaponProvider();
        Random.seed = pool.count;
	}

	public Entity CreateEnemyByModel(EnemyModel model) {
        getDifficultyController();
        EnemyModelComponent component = getModelIfExist(model.type);
        return component != null ? createEnemyFromComponent(model, component) : createEnemyFromCode(model);
    }

    public Entity CreateEnemyByType(int type, float posX, float posY, int health, int path, int grid, int damage = 10, float speed = 5.0f)
    {
        getDifficultyController();
        EnemyModelComponent component = getModelIfExist(type);

        return component != null 
            ? createEnemyByTypeFromComponent(component, type, posX, posY, health, path, grid, damage, speed)
            : createEnemyByTypeFromCode(type, posX, posY, health, path, grid, damage, speed);
    }

    Entity createEnemyFromCode(EnemyModel model) {
        return createEnemyByTypeFromCode(model.type, model.posX, model.posY, model.health, model.path, 0, model.damage, model.speed);
    }

    Entity createEnemyByTypeFromComponent(EnemyModelComponent component, int type, float posX, float posY, int health, int path, int grid, int damage, float velocityLimit) {
        Entity e = enemyCreator.createStandardEnemy(type, damage, posX, posY, health, velocityLimit, component.resource);
        e.AddFaceDirection(component.faceDirection);
        addCameraShakeIfNeeded(component, e);
        addRotationIfNeeded(component, e);
        addPathIfNeeded(e, posY, path);
        weaponProvider.provide(e, damage, component);
        return e;
    }

    Entity createEnemyByTypeFromCode(int type, float posX, float posY, int health, int path, int grid, int damage, float velocityLimit) {
        Entity e = null;
        float missileSpeedFactor = getMissileSpeedFactor();
        damage = (int)(damage * getDamageFactor());

        switch (type) {
            case EnemyTypes.MovingBlock:
                e = enemyCreator.createStandardEnemy(type, damage, posX, posY, health, velocityLimit, ResourceWithColliders.Blockade);
                e.AddMovingBlockade(2.0f, -1.0f, 0.0f, velocityLimit, velocityLimit / 2.0f);
                addPathIfNeeded(e, posY, path);
                addGridIfNeeded(e, grid);
                break;
            case EnemyTypes.MovingBlockade:
                for (int i = 10; i > 0; i--) {
                    if (i == 5) {
                        e = enemyCreator.createBlock(type, damage, new Vector2(-4.0f + (float)i, posY), 1, ResourceWithColliders.Meteor);
                    }
                    else {
                        e = enemyCreator.createBlock(type, damage, new Vector2(-4.0f + (float)i, posY), health, ResourceWithColliders.Blockade);
                    }
                    e.AddMovingBlockade(2.0f, -1.0f, 0.0f, velocityLimit, velocityLimit / 2.0f);
                }
                break;
            case EnemyTypes.MotherShip:
                e = enemyCreator.createMothership(type, posX, posY, health, missileSpeedFactor);
                break;
            case EnemyTypes.FirstBoss:
                e = bossCreator.createFirstBoss(type, posX, posY, health, missileSpeedFactor, getDamageFactor());
                break;
            case EnemyTypes.SecondBoss:
                e = bossCreator.createSecondBoss(type, posX, posY, health, missileSpeedFactor, getDamageFactor());
                break;
            default:
                e = enemyCreator.createStandardEnemy(type, damage, posX, posY, health, velocityLimit * missileSpeedFactor, ResourceWithColliders.Enemy);
                break;
        }
        return e;
    }

    Entity createEnemyFromComponent(EnemyModel model, EnemyModelComponent component)
    {
        Entity e = enemyCreator.createStandardEnemy(model, component.resource);
        e.AddFaceDirection(component.faceDirection);
        addCameraShakeIfNeeded(component, e);
        addRotationIfNeeded(component, e);
        addPathIfNeeded(e, model.posY, model.path);
        weaponProvider.provide(e, model.damage, component);
        return e;
    }

    void addRotationIfNeeded(EnemyModelComponent component, Entity e)
    {
        if (component.randomRotation > 0)
        {
            float randomAngle = Random.Range(-component.randomRotation, component.randomRotation);
            e.AddRotate(randomAngle, randomAngle);
        }
    }

    void addCameraShakeIfNeeded(EnemyModelComponent component, Entity e)
    {
        if (component.shakeCamera > 0)
            e.AddCameraShakeOnDeath(component.shakeCamera);
    }

	void addPathIfNeeded(Entity e, float offsetY, int path) {
		if (path > 0) {
			e.AddPath(0, offsetY, 0.0f, paths.GetEntities()[path - 1].pathModel);
		}
	}

	void addGridIfNeeded(Entity e, int grid) {
		if (grid > 0) {
			e.AddGridField(0.0f, 1.0f, GridFieldState.INACTIVE, grid, -1, -1);
		}
	}

    EnemyModelComponent getModelIfExist(int type) {
        return enemies.count >= type && type != 0 ? enemies.GetEntities()[type - 1].enemyModel : null;
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
        {
            difficultyController = difficulty.GetSingleEntity().difficultyController;
            enemyCreator.SetController(difficultyController);
            weaponProvider.SetController(difficultyController);
        }
        return difficultyController;
    }
}
