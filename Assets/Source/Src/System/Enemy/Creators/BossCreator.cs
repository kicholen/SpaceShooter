using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class BossCreator
{
    Pool pool;

    public BossCreator(Pool pool)
    {
        this.pool = pool;
    }

    public Entity createFirstBoss(int type, float posX, float posY, int health, float missileSpeedFactor)
    {
        Entity boss = pool.CreateEntity()
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
        children.Add(pool.CreateEntity()
                     .AddRelativePosition(0.5f, 0.5f)
                     .AddPosition(new Vector2(0.0f, 0.0f))
                     .AddChild(boss)
                     .AddHomeMissileSpawner(5.0f, 10f, 10, ResourceWithColliders.MissileEnemyHoming, 2.0f * missileSpeedFactor,
                        new Vector2(2.0f, 1.0f), 0.5f, 3.0f, CollisionTypes.Enemy)
                     .AddResource(Resource.Weapon));
        children.Add(pool.CreateEntity()
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

    void addNonRemovable(List<Entity> entities)
    {
        foreach (Entity e in entities)
        {
            e.isNonRemovable = true;
        }
    }
}
