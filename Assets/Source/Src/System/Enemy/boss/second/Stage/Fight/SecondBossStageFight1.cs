using Entitas;
using UnityEngine;

public class SecondBossStageFight1 : BossStage
{
    Pool pool;
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public SecondBossStageFight1(Pool pool)
    {
        this.pool = pool;
        timeLimit = 4.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        e.secondBoss.missileSpawn -= deltaTime;
        if (e.secondBoss.missileSpawn < 0.0f)
        {
            e.secondBoss.missileSpawn = 0.2f;
            spawnMissile(e);
        }
    }

    void spawnMissile(Entity e)
    {
        e.parent.children.Add(createMissile(e, Random.Range(-1.0f, 1.0f), Random.Range(0.4f, 1.5f)));
    }

    Entity createMissile(Entity e, float relativeX, float relativeY)
    {
        return pool.CreateEntity()
            .AddRelativePosition(relativeX, relativeY)
            .AddPosition(new Vector2(0.0f, 0.0f))
            .AddChild(e)
            .AddCollision(CollisionTypes.Enemy, 10)
            .AddResource(ResourceWithColliders.MissileEnemyCircle);
    }
}
