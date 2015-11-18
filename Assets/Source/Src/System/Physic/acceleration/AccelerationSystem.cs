using Entitas;
using UnityEngine;

public class AccelerationSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	const float EPSILON = 0.1f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Acceleration, Matcher.Velocity));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (var e in _group.GetEntities()) {
			applyAcceleration(e, deltaTime);
		}
	}
	
	void applyAcceleration(Entity e, float deltaTime) {
		AccelerationComponent acceleration = e.acceleration;
		VelocityComponent velocity = e.velocity;

		acceleration.x = getAcceleration(acceleration.x, acceleration.frictionX, deltaTime);
		acceleration.y = getAcceleration(acceleration.y, acceleration.frictionY, deltaTime);

		velocity.vel += new Vector2(acceleration.x * deltaTime, acceleration.y * deltaTime);

		if (acceleration.stopNearZero) { // todo
			int count = 0;
			if (Mathf.Abs(velocity.vel.x) < EPSILON) {
				velocity.vel.x = 0.0f;
				count++;
			}
			if (Mathf.Abs(velocity.vel.y) < EPSILON) {
				velocity.vel.y = 0.0f;
				count++;
			}
			if (count == 2) {
				e.RemoveAcceleration();
			}
		}
	}

	float getAcceleration(float value, float friction, float deltaTime) {
		float deltaFriction = deltaTime * friction;
		if (value > deltaFriction) {
			return value - deltaFriction;
		}
		if (value < -deltaFriction) {
			return value + deltaFriction;
		}

		return 0.0f;
	}
}
