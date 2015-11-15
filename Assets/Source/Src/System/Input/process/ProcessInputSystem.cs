using Entitas;
using UnityEngine;

public class ProcessInputSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _input;
	Vector3 temp = new Vector3();

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.MouseInput);
		_input = pool.GetGroup(Matcher.Input);
	}

	public void Initialize() {
		createEntity();
	}

	public void Execute() {
		Entity e = _group.GetSingleEntity();

		MouseInputComponent component = e.mouseInput;
		temp.Set(component.x, component.y, 0);
		temp = Camera.main.ScreenToWorldPoint(temp);

		InputComponent input = _input.GetSingleEntity().input;
		input.x = temp.x;
		input.y = temp.y;
		input.isDown = component.isDown;
	}

	void createEntity() {
		_pool.CreateEntity()
			.AddInput(temp.x, temp.y, false, true);
	}
}