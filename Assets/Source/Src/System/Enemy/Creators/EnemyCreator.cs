using Entitas;
using UnityEngine;

public class EnemyCreator
{
    Pool pool;
    DifficultyControllerComponent difficultyController;

    public EnemyCreator(Pool pool)
    {
        this.pool = pool;
    }

    public void SetController(DifficultyControllerComponent difficultyController)
    {
        this.difficultyController = difficultyController;
    }

    public Entity createStandardEnemy(EnemySpawnModel model, string resource)
    {
        float healthFactor = getHealthFactor();

        Entity e = pool.CreateEntity()
            .AddEnemy(model.type)
            .AddPosition(new Vector2(model.posX, model.posY))
            .AddVelocity(new Vector2())
            .AddVelocityLimit(model.speed)
            .AddResource(resource)
            .AddCollision(CollisionTypes.Enemy, model.damage)
            .IsNonRemovable(true)
            .IsActive(true);
        if (model.health >= 0)
        {
            e.AddHealth((int)(model.health * healthFactor))
                .AddBonusOnDeath(getAllBonuses())
                .AddExplosionOnDeath(1.0f, Resource.Explosion);
        }
        return e;
    }

    public Entity createStandardEnemy(int type, int damage, float posX, float posY, int health, float speedLimit, string resource)
    {
        Entity e = pool.CreateEntity()
            .AddEnemy(type)
            .AddPosition(new Vector2(posX, posY))
            .AddVelocity(new Vector2())
            .AddVelocityLimit(speedLimit)
            .AddResource(resource)
            .AddCollision(CollisionTypes.Enemy, damage)
            .IsNonRemovable(true)
            .IsActive(true);
        if (health >= 0)
        {
            e.AddHealth(health)
                .AddBonusOnDeath(getAllBonuses())
                .AddExplosionOnDeath(1.0f, Resource.Explosion);
        }
        return e;
    }

    public Entity createBlock(int type, int damage, Vector2 position, int health, string resource)
    {
        Entity e = pool.CreateEntity()
            .AddEnemy(type)
            .AddPosition(position)
            .AddResource(resource)
            .AddCollision(CollisionTypes.Enemy, damage);
        if (health >= 0)
        {
            e.AddHealth(health)
                .AddBonusOnDeath(getAllBonuses())
                .AddExplosionOnDeath(1.0f, Resource.Explosion);
        }
        return e;
    }

    public Entity createMothership(int type, float posX, float posY, int health, float speedLimit)
    {
        return pool.CreateEntity()
            .AddHealth(health)
            .AddVelocityLimit(0.0f)
            .AddVelocity(new Vector2())
            .AddCollision(CollisionTypes.Enemy, 5000)
            .AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
            .AddExplosionOnDeath(1.0f, Resource.Explosion)
            .AddResource(ResourceWithColliders.Blockade)
            .IsMoveWithCamera(true)
            .AddMotherShip(0.0f, 1.0f, 3.0f, 0, 4, 1, 1000, 1, 3.0f)
            .AddPosition(new Vector2(posX, posY));
    }

    float getHealthFactor()
    {
        return (difficultyController.hpBoostPercent + 100.0f) / 100.0f;
    }

    int getAllBonuses()
    {
        return BonusTypes.Star | BonusTypes.Speed | BonusTypes.Laser | BonusTypes.Shield | BonusTypes.Atom | BonusTypes.FireRate | BonusTypes.Damage | BonusTypes.HomingMissile;
    }
}
