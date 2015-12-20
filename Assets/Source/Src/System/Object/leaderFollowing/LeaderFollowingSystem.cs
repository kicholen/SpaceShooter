using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class HelperShipSystem : IExecuteSystem, ISetPool {
	Group _group;

	const float LEADER_BEHIND_DIST = 0.5f;
	const float SLOWING_RADIUS = 1.6f;
	const float STEERING = 8.0f;
	const float SEPERATION_RADIUS = 0.5f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.LeaderFollower, Matcher.Child));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			Entity parent = e.child.parent;
			float speed = e.velocityLimit.maxVelocity;
			VelocityComponent velocity = e.velocity;

			Vector2 force = seperationSteeringForce(e, e.position, parent.parent.children) * 2.2f;
			force += arrivalSteeringForce(e.position, velocity, speed, getDesiredPosition(parent.velocity, parent.position), SLOWING_RADIUS);
			force.Normalize();
			force *= speed / STEERING;
			velocity.vel += force;
		}
	}

	Vector2 getDesiredPosition(VelocityComponent velocity, PositionComponent position) {
		Vector2 positionOffset = velocity.vel;
		positionOffset *= -1.0f;
		positionOffset.Normalize();
		positionOffset *= LEADER_BEHIND_DIST;

		return position.pos + positionOffset;
	}

	Vector2 arrivalSteeringForce(PositionComponent position, VelocityComponent velocity, float speed, Vector2 desiredPosition, float slowingRadius) {
		Vector2 desiredVelocity = new Vector2((desiredPosition.x - position.pos.x), (desiredPosition.y - position.pos.y));
		float distance = desiredVelocity.magnitude;
		desiredVelocity.Normalize();
		desiredVelocity *= speed;
		if (distance < slowingRadius) {
			desiredVelocity *= (distance / slowingRadius);
		}
		return desiredVelocity - velocity.vel;
	}

	Vector2 seperationSteeringForce(Entity e, PositionComponent position, List<Entity> children) {
		Vector2 desiredVelocity = new Vector2();
		int neighbourCount = 0;
		for (int i = 0; i < children.Count; i++) {
			Entity child = children[i];
			if (child.isLeaderFollower && child != e) {
				Vector2 diff = position.pos - child.position.pos;
				if (diff.sqrMagnitude < SEPERATION_RADIUS*SEPERATION_RADIUS) {
					desiredVelocity += diff;
				}
			}
		}
		return desiredVelocity;
	}
}