using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class VelocitySystem : IExecuteSystem, ISetPool {
	Group _group;
	float _deltaTime;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Acceleration, Matcher.Velocity));
	}
	
	public void Execute() {
		Debug.Log("VelocitySystem");
		_deltaTime = Time.deltaTime;
		foreach (var e in _group.GetEntities()) {
			applyVelocity(e);
			limitVelocity(e);
		}
	}

	void applyVelocity(Entity e) {
		AccelerationComponent acceleration = e.acceleration;
		VelocityComponent velocity = e.velocity;

		e.ReplaceVelocity(velocity.x + _deltaTime * acceleration.x, velocity.y + _deltaTime * acceleration.y);
	}

	void limitVelocity(Entity e) {
		if (!e.hasVelocityLimit) {
			return;
		}
		VelocityLimitComponent velocityLimit = e.velocityLimit;
		VelocityComponent velocity = e.velocity;

		if (velocity.x > velocityLimit.x) {
			velocity.x = velocityLimit.x;
		}
		else if (velocity.x < -velocityLimit.x) {
			velocity.x = -velocityLimit.x;
		}

		if (velocity.y > velocityLimit.y) {
			velocity.y = velocityLimit.y;
		}
		else if (velocity.y < -velocityLimit.y) {
			velocity.y = -velocityLimit.y;
		}
	}
}
