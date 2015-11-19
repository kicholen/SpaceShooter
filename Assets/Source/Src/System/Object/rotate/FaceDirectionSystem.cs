using Entitas;
using UnityEngine;
using System;

public class FaceDirectionSystem : IExecuteSystem, ISetPool {

	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Velocity, Matcher.FaceDirection, Matcher.GameObject));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;

		foreach (Entity e in _group.GetEntities()) {
			Vector2 velocity = e.velocity.vel;
			GameObjectComponent gameObject = e.gameObject;
			float angle = (float)Math.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90.0f;
			Transform transform = gameObject.gameObject.transform;

			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
}