using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class ProcessInputSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.Input.OnEntityAdded(); } }

	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}

	public void Execute(List<Entity> entities) {
		Debug.Log("ProcessInputSystem");
		Entity e = entities.SingleEntity();
		InputComponent component = e.input;
		createTestEntity();
		_pool.DestroyEntity(e);
	}

	void createTestEntity() {
		_pool.CreateEntity()
			.AddResource(Resource.Test);
	}
}