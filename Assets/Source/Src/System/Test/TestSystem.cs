using Entitas;
using UnityEngine;

public class TestSystem : IInitializeSystem, IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _playerGroup;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.Test);
		_playerGroup = pool.GetGroup(Matcher.Player);
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
		Group group = _pool.GetGroup(Matcher.Camera);
		Entity e = group.GetSingleEntity();
		Entity player = _playerGroup.GetSingleEntity();

		if (e.hasSmoothCamera) {
			e.AddRegularCamera(e.smoothCamera.camera, e.smoothCamera.offset)
				.RemoveVelocity()
				.RemovePosition();
			e.RemoveSmoothCamera();
		}
		else if (e.hasRegularCamera) {
			e.AddSmoothCamera(e.regularCamera.camera, e.regularCamera.offset)
				.AddVelocity(0.0f, 2.0f)
				.AddPosition(player.position.x, player.position.y + e.regularCamera.offset.y);
			e.RemoveRegularCamera();
		}
	}
}