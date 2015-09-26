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
		}
	}

	void applyVelocity(Entity e) {
		AccelerationComponent acceleration = e.acceleration;
		VelocityComponent velocity = e.velocity;

		e.ReplaceVelocity(velocity.x + _deltaTime * acceleration.x, velocity.y + _deltaTime * acceleration.y);
	}
}
