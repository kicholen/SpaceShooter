using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Path, Matcher.GameObject, Matcher.Velocity, Matcher.Position));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			PathComponent path = e.path;
			List<Vector2> points = path.path.points;
			if (path.node < points.Count) {
				VelocityComponent velocity = e.velocity;
				PositionComponent futurePosition = e.position;
				Vector3 currentPosition = e.gameObject.gameObject.transform.position;
				Vector2 desiredPosition = points[path.node] + new Vector2(0.0f, path.startY);

				velocity.vel.Set((desiredPosition.x - futurePosition.pos.x), (desiredPosition.y - futurePosition.pos.y));
				velocity.vel.Normalize();
				velocity.vel *= 3.0f;

				if (isPointBetween(currentPosition, desiredPosition, futurePosition)) {
					path.node = path.node + 1;
				}
			}
			else {
				e.RemovePath();
			}
		}
	}

	bool isPointBetween(Vector3 current, Vector2 desired, PositionComponent future) {
		return within(current.x, desired.x, future.pos.x) && within(current.y, desired.y, future.pos.y);
	}

	bool within(float current, float desired, float future) {
		return (current <= desired && desired <= future) || (future <= desired && desired <= current);
	}
	
}