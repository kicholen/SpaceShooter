using Entitas;
using UnityEngine;

public class DispersionMissileSpawnerSystem : IExecuteSystem, ISetPool {
    Pool _pool;
    Group _time;
    Group _missiles;

    public void SetPool(Pool pool) {
        _pool = pool;
        _missiles = _pool.GetGroup(Matcher.DispersionMissileSpawner);
        _time = pool.GetGroup(Matcher.Time);
    }

    public void Execute() {
        float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
        foreach (Entity e in _missiles.GetEntities()) {
            DispersionMissileSpawnerComponent missile = e.dispersionMissileSpawner;
            missile.time -= deltaTime;
            if (missile.time < 0.0f) {
                missile.time -= deltaTime;
                if (missile.time < 0.0f) {
                    missile.time = missile.spawnDelay;
                    spawnMissile(missile, e.position.pos);
                }
            }
        }
    }

    void spawnMissile(DispersionMissileSpawnerComponent missile, Vector2 position) {
        float angle = missile.angle / 90.0f;

        for (int i = 0; i < 2; i++) {
            _pool.CreateEntity()
                .AddPosition(new Vector2(position.x, position.y))
                .AddVelocity(new Vector2(missile.velocity * angle, missile.velocity))
                .AddHealth(0)
                .AddCollision(missile.collisionType, missile.damage)
                .AddResource(missile.resource);
            angle = -angle;
        }
    }

}
