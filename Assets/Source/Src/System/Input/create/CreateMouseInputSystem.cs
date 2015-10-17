using Entitas;
using UnityEngine;

public class CreateMouseInputSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	Pool _pool;
	Group _group;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.MouseInput);
	}

	public void Initialize() {
		generateInputComponent();
	}

	public void Execute() {
		Entity e = _group.GetSingleEntity();
		MouseInputComponent input = e.mouseInput;

		input.x = Input.mousePosition.x;
		input.y = Input.mousePosition.y;
		input.isDown = isMouseButton();
	}

	bool isMouseButton() {
		return Input.GetMouseButton (0);
	}

	void generateInputComponent() {
		_pool.CreateEntity()
			.AddMouseInput(Input.mousePosition.x, Input.mousePosition.y, isMouseButton());
	}
}