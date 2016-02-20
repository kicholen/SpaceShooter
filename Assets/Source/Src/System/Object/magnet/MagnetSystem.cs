using Entitas;
using UnityEngine;

public class MagnetSystem : IExecuteSystem, ISetPool {
    const float POWER_OF_TWO = 2.0f;

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
                if (isPointInCircle(targetPosition.x, targetPosition.y, magnetComponent.radius, position.x, position.y))
                {
                    if (e.hasTween)
                        e.RemoveTween();
                    velocity.vel.Set((targetPosition.x - position.x), (targetPosition.y - position.y));
                }
                else
                {
                    velocity.vel.Set(0.0f, 0.0f);
                }
            }
		}
	}

	bool isPointInCircle(float centerX, float centerY, float radiusPower, float x, float y) {
		float distance = Mathf.Pow(centerX - x, POWER_OF_TWO) + Mathf.Pow(centerY - y, POWER_OF_TWO);
		return distance < radiusPower;
	}
}