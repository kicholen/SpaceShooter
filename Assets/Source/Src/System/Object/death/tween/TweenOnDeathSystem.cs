using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class TweenOnDeathSystem : IReactiveSystem, ISetPool {
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.TweenOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }

    Pool _pool;

    public void SetPool(Pool pool) {
        _pool = pool;
    }

    public void Execute(List<Entity> entities) {
        for (int i = 0; i < entities.Count; i++) {
            Entity e = entities[i];
            TweenOnDeathComponent component = e.tweenOnDeath;

            addTweenIfDoesntExist(e);
            removeComponents(e);
            e.IsDestroyEntity(false);
            addScaleUpTween(e, component);
        }
    }

    void addTweenIfDoesntExist(Entity e) {
        if (!e.hasTween) {
            e.AddTween(true, new List<Tween>());
        }
    }

    void removeComponents(Entity e) {
        e.RemovePosition();
        e.RemoveVelocity();
        e.RemoveCollision();
        e.RemoveMagnet();
        e.RemoveTweenOnDeath();
        e.RemoveHealth();
        e.RemoveFollowTarget();
        e.IsCollisionDeath(false);
    }

    void addScaleUpTween(Entity e, TweenOnDeathComponent component) {
        Vector3 localScale = e.gameObject.gameObject.transform.localScale;
        e.tween.AddTween(e.gameObject, EaseTypes.linear, GameObjectAccessorType.LOCAL_SCALE_XY, component.duration)
            .From(localScale.x, localScale.y)
            .To(localScale.x * component.offset, localScale.y * component.offset)
            .SetEndCallback(entity => entity.IsDestroyEntity(true));
    }
}