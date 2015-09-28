using Entitas;
using UnityEngine;

public class TestSystem : IInitializeSystem, IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.Test);
	}

	public void Initialize() {
		_pool.CreateEntity()
			.isTest = true;
	}

	public void Execute() {
		Debug.Log("TestSystem");
		Entity e = _group.GetSingleEntity();

		if (Input.GetKeyDown(KeyCode.C)) {
			changeCamera();
		}
	}
	
	void changeCamera() {
		Group group = _pool.GetGroup(Matcher.Player);
		Entity e = group.GetSingleEntity();

		if (e.hasSmoothCamera) {
			e.AddCamera(e.smoothCamera.camera, e.smoothCamera.offset);
			e.RemoveSmoothCamera();
		}
		else if (e.hasCamera) {
			e.AddSmoothCamera(e.camera.camera, e.camera.offset);
			e.RemoveCamera();
		}
	}
}