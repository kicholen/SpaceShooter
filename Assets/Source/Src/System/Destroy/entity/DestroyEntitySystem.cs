using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class DestroyEntitySystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.DestroyEntity.OnEntityAdded(); } }

	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}

	public void Execute(List<Entity> entities) {
		Debug.Log("DestroyEntitySystem");
		for (int i = 0; i < entities.Count; i++) {
			_pool.DestroyEntity(entities[i]);
		}
	}
}
