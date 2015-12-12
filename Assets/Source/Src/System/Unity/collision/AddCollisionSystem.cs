using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddCollisionToGameObjectSystem : IReactiveSystem {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.GameObject, Matcher.Collision).OnEntityAdded(); } }
	
	public void Execute(List<Entity> entities) {
		for (int i = 0; i < entities.Count; i++) {
			Entity e = entities[i];
			GameObject go = e.gameObject.gameObject;
			CollisionScript script = go.GetComponent<CollisionScript>();
			if (script == null) {
				script = go.AddComponent<CollisionScript>();
			}
			script.Damage = e.collision.damage;
			script.DamageTaken = 0;
		}
	}
}
