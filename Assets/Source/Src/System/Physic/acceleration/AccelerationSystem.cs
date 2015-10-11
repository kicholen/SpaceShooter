using UnityEngine;
using Entitas;

public class AccelerationSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Acceleration, Matcher.Velocity));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		Debug.Log("AccelerationSystem");
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (var e in _group.GetEntities()) {
			applyFriction(e, deltaTime);
		}
	}
	
	void applyFriction(Entity e, float deltaTime) {
		AccelerationComponent acceleration = e.acceleration;

		acceleration.x = getAcceleration(acceleration.x, acceleration.frictionX, deltaTime);
		acceleration.y = getAcceleration(acceleration.y, acceleration.frictionY, deltaTime);
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
