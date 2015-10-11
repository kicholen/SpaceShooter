using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class DeadPlayerSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.DestroyEntity, Matcher.Player).OnEntityAdded(); } }
	
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}

	public void Execute(List<Entity> entities) {
		Debug.Log("DeadPlayerSystem");

		foreach (Entity e in entities) {
			if (e.isDestroyEntity) {
				createRestartEntity();
				e.isDestroyEntity = false;
			}
		}

	}

	void createRestartEntity() {
		_pool.CreateEntity()
			.isRestartGame = true;
	}
}
