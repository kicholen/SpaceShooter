using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class ProcessInputSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.MouseInput.OnEntityAdded(); } }

	Pool _pool;
	Vector3 temp = new Vector3();

	public void SetPool(Pool pool) {
		_pool = pool;
	}

	public void Execute(List<Entity> entities) {
		Debug.Log("ProcessInputSystem");
		Entity e = entities.SingleEntity();

		if (e.hasMouseInput) {
			handleMouseInput(e.mouseInput);
		}
	}

	void createTestEntity(InputComponent component) {
		_pool.CreateEntity()
			.AddPosition(component.x, component.y)
			.AddResource(Resource.Test);
	}	

	void handleMouseInput(MouseInputComponent component) {
		temp.Set(component.x, component.y, 0);
		temp = Camera.main.ScreenToWorldPoint(temp);

		_pool.CreateEntity()
			.AddInput(temp.x, temp.y)
			.isDestroyEntity = true;
	}
}