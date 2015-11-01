using Entitas;
using UnityEngine;
using System;

public class FaceDirectionSystem : IExecuteSystem, ISetPool {
	Group _group;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Velocity, Matcher.FaceDirection, Matcher.GameObject));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			Vector2 velocity = e.velocity.vel;
			GameObjectComponent gameObject = e.gameObject;

			float angle = (float)Math.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

			gameObject.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
}