using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnDeathSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.ExplosionOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }
	
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities) {
		for (int i = 0; i < entities.Count; i++) {
			Entity e = entities[i];
			ExplosionOnDeathComponent component = e.explosionOnDeath;
			Vector2 position = e.position.pos;
			_pool.CreateEntity()
				.AddPosition (new Vector2 (position.x, position.y))
				.AddDestroyEntityDelayed(component.lifetime)
				.AddResource(component.resource);
		}
	}
}