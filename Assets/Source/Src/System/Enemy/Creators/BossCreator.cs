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
            .AddTween(true, new List<Tween>())
            .AddFirstBoss(22.0f, 0.0f, 90.0f);
        boss.isMoveWithCamera = true;

        boss.tween.AddTween(boss.position, EaseTypes.bounceIn, PositionAccessorType.X, 5.0f)
            .From(posX)
            .To(posX + 5f)
            .PingPong();

        boss.AddParent(new List<Entity>());
        return boss;
    }
}
