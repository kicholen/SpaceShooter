using Entitas;
using UnityEngine;

public class TargetMissileSpawnerSystem : IExecuteSystem, ISetPool {
    Pool pool;
    Group group;
    Group time;

    public void SetPool(Pool pool) {
        this.pool = pool;
        group = this.pool.GetGroup(Matcher.TargetMissileSpawner);
        time = pool.GetGroup(Matcher.Time);
    }

    public void Execute() {
        float deltaTime = time.GetSingleEntity().time.gameDeltaTime;
        foreach (Entity e in group.GetEntities()) {
            TargetMissileSpawnerComponent missile = e.targetMissileSpawner;
            missile.time -= deltaTime;
            spawnMissileIfDelayReached(deltaTime, e, missile);
        }
    }

    void spawnMissileIfDelayReached(float deltaTime, Entity e, TargetMissileSpawnerComponent missile) {
        if (missile.time < 0.0f) {
            missile.time = missile.spawnDelay;
            spawnMissile(missile, e.position.pos);
        }
    }

    void spawnMissile(TargetMissileSpawnerComponent missile, Vector2 position) {
        pool.CreateEntity()
            .AddPosition(new Vector2(position.x, position.y))
            .AddVelocityLimit(missile.velocity)
            .AddHealth(0)
            .AddFindTarget(missile.targetCollisionType)
            .AddCollision(missile.collisionType, missile.damage)
            .AddFaceDirection(false)
            .IsTargetMissile(true)
            .AddResource(missile.resource);
    }
}
