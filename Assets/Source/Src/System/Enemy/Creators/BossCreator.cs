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
            .AddVelocityLimit(0.0f)
            .AddCollision(CollisionTypes.Enemy, health)
            .AddHealth(health)
            .AddResource(ResourceWithColliders.Boss)
            .AddFirstBoss(22.0f, 0.0f, 90.0f, 0);
        boss.isMoveWithCamera = true;

        boss.AddParent(new List<Entity>());
        return boss;
    }
}
