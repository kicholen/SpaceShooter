using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class HelperShipSystem : IExecuteSystem, ISetPool {
	Group _group;

	const float LEADER_BEHIND_DIST = 0.5f;
	const float SLOWING_RADIUS = 0.6f;
	const float SEPARATION_RADIUS = 0.5f;
	const float MAX_SEPERATION = 2.5f;
	const float STEERING = 8.0f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.LeaderFollower, Matcher.Child));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			Entity parent = e.child.parent;
			float speed = e.velocityLimit.maxVelocity;
			
			VelocityComponent velocity = e.velocity;
			Vector2 steering = getSteering(e.position, velocity, speed, getDesiredPosition(parent.velocity, parent.position), SLOWING_RADIUS);
			steering += getSeperation(e, parent.parent.children);

			steering.Normalize();
			steering *= speed / STEERING;

			velocity.vel += steering;
		}
	}

	Vector2 getDesiredPosition(VelocityComponent velocity, PositionComponent position) {
		Vector2 parentVelocity = velocity.vel;
		parentVelocity *= -1;
		parentVelocity.Normalize();
		parentVelocity *= LEADER_BEHIND_DIST;

		return position.pos + parentVelocity;
	}

	Vector2 getSteering(PositionComponent position, VelocityComponent velocity, float speed, Vector2 desiredPosition, float slowingRadius) {
		Vector2 desiredVelocity = new Vector2((desiredPosition.x - position.pos.x), (desiredPosition.y - position.pos.y));
		float distance = desiredVelocity.magnitude;
		desiredVelocity.Normalize();
		desiredVelocity *= speed;
		if (distance < slowingRadius) {
			desiredVelocity *= (distance / slowingRadius);
		}
		return desiredVelocity - velocity.vel;
	}

	Vector2 getSeperation(Entity e, List<Entity> children) {
		Vector2 seperation = new Vector2();
		Vector2 position = e.position.pos;

		int neighborCount = 0;
		for (int i = 0; i < children.Count; i++) {
			Entity child = children[i];
			if (child.isLeaderFollower && child != e) {
				Vector2 childPosition = child.position.pos;
				if (Vector2.Distance(position, childPosition) <= SEPARATION_RADIUS) {
					seperation.x += childPosition.x - position.x;
					seperation.y += childPosition.y - position.y;
					neighborCount++;
				}
			}
		}
		
		if (neighborCount != 0) {
			seperation.x /= neighborCount;
			seperation.y /= neighborCount;
			seperation *= -1;
		}
		
		seperation.Normalize();
		seperation *= MAX_SEPERATION;

		return seperation;
	}
}