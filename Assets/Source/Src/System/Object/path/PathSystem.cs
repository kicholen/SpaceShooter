using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : IExecuteSystem, ISetPool {
	Group _group;

	const float MIN_DISTANCE = 0.1f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Path, Matcher.GameObject, Matcher.Velocity, Matcher.VelocityLimit, Matcher.Position));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			PathComponent path = e.path;
			List<Vector2> points = path.path.points;
			if (path.node != points.Count) {
				Vector2 currentPosition = e.position.pos;
				Vector2 desiredPosition = points[path.node] + new Vector2(0.0f, path.startY);
				float speed = e.velocityLimit.maxVelocity;

				VelocityComponent velocity = e.velocity;
				velocity.vel.Set((desiredPosition.x - currentPosition.x), (desiredPosition.y - currentPosition.y));
				velocity.vel.Normalize();
				velocity.vel *= speed;
				if (distance(currentPosition, desiredPosition) <= MIN_DISTANCE) {
					path.node = path.node + 1;
				}
				/*if (isPointBetween(currentPosition, desiredPosition, futurePosition)) {

				}*/
			}
			else {
				e.RemovePath();
			}
		}
	}


	float distance(Vector2 current, Vector2 desired) {
		return Mathf.Sqrt((current.x - desired.x) * (current.x - desired.x) + (current.y - desired.y) * (current.y - desired.y));
	}
}