using System;
using Entitas;
using UnityEngine;

public class MotherShipSystem : IExecuteSystem, ISetPool
{
    Pool pool;
    Group group;
    Group time;
    Group enemyFactory;

    EnemyFactory factory;

    public void SetPool(Pool pool)
    {
        this.pool = pool;
        group = pool.GetGroup(Matcher.AllOf(Matcher.MotherShip, Matcher.Position));
        time = pool.GetGroup(Matcher.Time);
        enemyFactory = pool.GetGroup(Matcher.EnemyFactory);
    }

    public void Execute()
    {
        float deltaTime = time.GetSingleEntity().time.gameDeltaTime;

        foreach (Entity e in group.GetEntities())
        {
            MotherShipComponent component = e.motherShip;

            component.time -= deltaTime;
            if (component.time < 0.0f)
            {
                component.time = component.duration + UnityEngine.Random.Range(0.0f, component.timeRandomFactor);
                spawnEnemy(e.motherShip, e.position);
                increaseSpawnCountAndRemoveIfMaxed(e, e.motherShip);
            }
        }
    }

    void increaseSpawnCountAndRemoveIfMaxed(Entity e, MotherShipComponent motherShip)
    {
        if (++motherShip.spawnedDronesCount >= motherShip.droneSpawnLimit)
            e.isDestroyEntity = true;
    }

    void spawnEnemy(MotherShipComponent motherShip, PositionComponent position)
    {
        getFactory().CreateEnemyByType(motherShip.droneType, position.pos.x, position.pos.y, motherShip.droneHealth, 0, 0, motherShip.droneDamage, motherShip.droneSpeed)
            .ReplaceVelocity(getRandomizedStartVelocity(motherShip.droneSpeed))
            .AddFindTarget(CollisionTypes.Player)
            .AddHomeMissile(0.5f, motherShip.droneSpeed, CollisionTypes.Player);
    }

    Vector2 getRandomizedStartVelocity(float droneSpeed)
    {
        return new Vector2(UnityEngine.Random.value > 0.5f ? droneSpeed : -droneSpeed, UnityEngine.Random.Range(-0.2f, 0.2f));
    }

    EnemyFactory getFactory()
    {
        if (factory == null)
            factory = enemyFactory.GetSingleEntity().enemyFactory.factory;
        return factory;
    }
}
