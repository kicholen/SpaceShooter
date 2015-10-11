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
		Entity e = entities.SingleEntity();

		handleMouseInput(e.mouseInput);
	}

	void handleMouseInput(MouseInputComponent component) {
		temp.Set(component.x, component.y, 0);
		temp = Camera.main.ScreenToWorldPoint(temp);

		_pool.CreateEntity()
			.AddInput(temp.x, temp.y, component.isDown)
			.isDestroyEntity = true;
	}
}