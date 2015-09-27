using Entitas;
using UnityEngine;

public class CreateMouseInputSystem : IExecuteSystem, ISetPool {
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Execute() {
		generateInputComponent();
	}

	bool isMouseButton() {
		return Input.GetMouseButton (0);
	}

	void generateInputComponent() {
		_pool.CreateEntity()
			.AddMouseInput(Input.mousePosition.x, Input.mousePosition.y, isMouseButton())
			.isDestroyEntity = true;
	}
}