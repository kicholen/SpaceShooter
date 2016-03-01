using Entitas;
using UnityEngine;

public class MagnetSystem : IExecuteSystem, ISetPool {

    Group group;

	public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.AllOf(Matcher.Magnet, Matcher.FollowTarget, Matcher.Velocity, Matcher.Position));
	}
	
	public void Execute() {
		foreach (Entity e in group.GetEntities()) {
			Entity target = e.followTarget.target;
			if (target != null && target.hasPosition) {
				Vector2 targetPosition = target.position.pos;
				MagnetComponent magnetComponent = e.magnet;
				Vector2 position = e.position.pos;

                VelocityComponent velocity = e.velocity;
                if (Vector2.Distance(targetPosition, position) <= magnetComponent.radius)
                {
                    removeTweenIfExist(e);
                    velocity.vel = targetPosition - position;
                }
                else
                    velocity.vel.Set(Vector2.zero);
            }
		}
	}

    void removeTweenIfExist(Entity e)
    {
        if (e.hasTween)
            e.RemoveTween();
    }
}