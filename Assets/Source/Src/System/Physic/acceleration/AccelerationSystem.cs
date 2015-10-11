using Entitas;
using UnityEngine;

public class AccelerationSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	const float EPSILON = 0.05f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Acceleration, Matcher.Velocity));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (var e in _group.GetEntities()) {
			applyAcceleration(e, deltaTime);
		}
	}
	
	void applyAcceleration(Entity e, float deltaTime) {
		AccelerationComponent acceleration = e.acceleration;
		VelocityComponent velocity = e.velocity;

		acceleration.x = getAcceleration(acceleration.x, acceleration.frictionX, deltaTime);
		acceleration.y = getAcceleration(acceleration.y, acceleration.frictionY, deltaTime);

		velocity.x = velocity.x + deltaTime * acceleration.x;
		velocity.y = velocity.y + deltaTime * acceleration.y;

		if (acceleration.stopNearZero) {
			int count = 0;
			if (Mathf.Abs(velocity.x) < EPSILON) {
				velocity.x = 0.0f;
				count++;
			}
			if (Mathf.Abs(velocity.y) < EPSILON) {
				velocity.y = 0.0f;
				count++;
			}
			if (count == 2) {
				e.RemoveAcceleration();
			}
		}
	}

	float getAcceleration(float value, float friction, float deltaTime) {
		if (value > friction) {
			return value - deltaTime * friction;
		}
		if (value < -friction) {
			return value + deltaTime * friction;
		}

		return 0.0f;
	}
}
