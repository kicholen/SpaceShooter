using UnityEngine;
using Entitas;

public class AccelerationSystem : IExecuteSystem, ISetPool {
	Group _group;
	float _deltaTime;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Acceleration, Matcher.Velocity));
	}
	
	public void Execute() {
		Debug.Log("AccelerationSystem");
		_deltaTime = Time.deltaTime;
		foreach (var e in _group.GetEntities()) {
			applyFriction(e);
		}
	}
	
	void applyFriction(Entity e) {
		AccelerationComponent acceleration = e.acceleration;

		acceleration.x = getAcceleration(acceleration.x, acceleration.frictionX);
		acceleration.y = getAcceleration(acceleration.y, acceleration.frictionY);
	}

	float getAcceleration(float value, float friction) {
		if (value > friction) {
			return value - _deltaTime * friction;
		}
		if (value < -friction) {
			return value + _deltaTime * friction;
		}

		return 0.0f;
	}
}
