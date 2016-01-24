using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class TargetMissileSystem : IReactiveSystem {
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.TargetMissile, Matcher.FollowTarget).OnEntityAdded(); } }

    public void Execute(List<Entity> entities) {
        foreach (Entity e in entities) {
            navigateMissileOrDestroy(e);
        }
    }

    void navigateMissileOrDestroy(Entity e) {
        if (!hasFoundTarget(e.followTarget)) {
            e.isDestroyEntity = true;
            return;
        }
        else {
            e.AddVelocity(new Vector2());
            setVelocityBasedOnTarget(e);
            e.RemoveFollowTarget();
        }
    }

    bool hasFoundTarget(FollowTargetComponent targetComponent) {
        return targetComponent.target != null && targetComponent.target.hasPosition;
    }

    void setVelocityBasedOnTarget(Entity e) {
        Vector2 position = e.position.pos;
        Vector2 targetPosition = e.followTarget.target.position.pos;
        VelocityComponent velocity = e.velocity;
        VelocityLimitComponent velocityLimit = e.velocityLimit;

        velocity.vel.Set(targetPosition.x - position.x, targetPosition.y - position.y);
        velocity.vel.Normalize();
        velocity.vel *= velocityLimit.maxVelocity;
    }
}
