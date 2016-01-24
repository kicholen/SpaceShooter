using Entitas;
using UnityEngine;

public class TargetMissileSpawnerSystem : IExecuteSystem, ISetPool {
    Pool _pool;
    Group _time;
    Group _missiles;

    public void SetPool(Pool pool) {
        _pool = pool;
        _missiles = _pool.GetGroup(Matcher.TargetMissileSpawner);
        _time = pool.GetGroup(Matcher.Time);
    }

    public void Execute() {
        float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
        foreach (Entity e in _missiles.GetEntities()) {
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
        _pool.CreateEntity()
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
