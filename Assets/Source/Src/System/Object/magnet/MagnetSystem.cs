using Entitas;
using UnityEngine;

public class MagnetSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Magnet, Matcher.FollowTarget, Matcher.Velocity, Matcher.Position));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _group.GetEntities()) {
			Entity target = e.followTarget.target;
			if (target != null) {
				PositionComponent targetPosition = target.position;
				MagnetComponent magnetComponent = e.magnet;
				PositionComponent position = e.position;

				if (isPointInCircle(targetPosition.x, targetPosition.y, magnetComponent.radius, position.x, position.y)) { // todo it's calculating all, view seperation in quads?
					VelocityComponent velocity = e.velocity;
					velocity.x = (targetPosition.x - position.x) * 5.0f;
					velocity.y = (targetPosition.y - position.y) * 5.0f;
				}
			}
		}
	}

	bool isPointInCircle(float centerX, float centerY, float radiusPower, float x, float y) {
		float distance = Mathf.Pow(centerX - x, 2.0f) + Mathf.Pow(centerY - y, 2.0f);
		return distance < radiusPower;
	}
}