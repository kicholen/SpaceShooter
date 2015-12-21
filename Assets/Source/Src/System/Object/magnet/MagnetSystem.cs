using Entitas;
using UnityEngine;

public class MagnetSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Magnet, Matcher.FollowTarget, Matcher.Velocity, Matcher.Position));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			Entity target = e.followTarget.target;
			if (target != null && target.hasPosition) {
				Vector2 targetPosition = target.position.pos;
				MagnetComponent magnetComponent = e.magnet;
				Vector2 position = e.position.pos;

				if (isPointInCircle(targetPosition.x, targetPosition.y, magnetComponent.radius, position.x, position.y)) {
					if (e.hasTween) {
						e.RemoveTween();
					}
					VelocityComponent velocity = e.velocity;
					velocity.vel.Set((targetPosition.x - position.x) * 5.0f, (targetPosition.y - position.y) * 5.0f);
				}
			}
		}
	}

	bool isPointInCircle(float centerX, float centerY, float radiusPower, float x, float y) {
		float distance = Mathf.Pow(centerX - x, 2.0f) + Mathf.Pow(centerY - y, 2.0f);
		return distance < radiusPower;
	}
}