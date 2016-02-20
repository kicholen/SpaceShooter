using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AtomBombSystem : IReactiveSystem, ISetPool
{
    public TriggerOnEvent trigger { get { return Matcher.AtomBomb.OnEntityAdded(); } }

    const float flyDuration = 1.0f;

    Group enemy;
    Group player;
    Group camera;

    public void SetPool(Pool pool)
    {
        enemy = pool.GetGroup(Matcher.Enemy);
        player = pool.GetGroup(Matcher.Player);
        camera = pool.GetGroup(Matcher.Camera);
    }

    public void Execute(List<Entity> entities)
    {
        foreach(Entity e in entities)
        {
            Vector2 playerPosition = player.GetSingleEntity().position.pos;
            Vector2 cameraPosition = camera.GetSingleEntity().position.pos;

            e.AddResource(Resource.Particle)
                .AddCameraShakeOnDeath(1)
                .AddPosition(playerPosition)
                .AddExplosionOnDeath(1.0f, Resource.AtomBomb)
                .AddTween(true, new System.Collections.Generic.List<Tween>());

            e.tween.AddTween(e.position, EaseTypes.cubicIn, PositionAccessorType.XY, flyDuration)
                .From(playerPosition.x, playerPosition.y)
                .To(cameraPosition.x, cameraPosition.y + Config.CAMERA_SPEED * flyDuration)
                .SetEndCallback(destroyAllEnemies);
        }
    }

    void destroyAllEnemies(Entity entity)
    {
        destroyEntityAsCollision(entity);

        foreach (Entity e in enemy.GetEntities())
            destroyEntityAsCollision(e);
    }

    void destroyEntityAsCollision(Entity entity)
    {
        entity.isDestroyEntity = true;
        entity.isCollisionDeath = true;
    }
}