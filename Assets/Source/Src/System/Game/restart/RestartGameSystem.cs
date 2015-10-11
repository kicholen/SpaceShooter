using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class RestartGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.RestartGame.OnEntityAdded(); } }
	
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities) {
		Debug.Log("RestartGameSystem");
		
		foreach (Entity e in entities) {
			e.isDestroyEntity = false;
		}
	}
}
