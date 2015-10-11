using Entitas;
using UnityEngine;

public class TestSystem : IInitializeSystem, IExecuteSystem, ISetPool {
	const float EPSILON = 0.005f;

	Pool _pool;
	Group _playerGroup;
	Group _timeGroup;

	public void SetPool(Pool pool) {
		_pool = pool;
		_playerGroup = pool.GetGroup(Matcher.Player);
		_timeGroup = pool.GetGroup(Matcher.Time);
	}

	public void Initialize() {
		_pool.CreateEntity()
			.isTest = true;
	}

	public void Execute() {
		Debug.Log("TestSystem");

		if (Input.GetKeyDown(KeyCode.C)) {
			changeCamera();
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			changeTime();
		}
	}
	
	void changeCamera() {
		Group group = _pool.GetGroup(Matcher.Camera);
		Entity e = group.GetSingleEntity();
		Entity player = _playerGroup.GetSingleEntity();

		if (e.hasSmoothCamera) {
			e.AddRegularCamera(e.smoothCamera.offset)
				.RemoveVelocity()
				.RemovePosition();
			e.RemoveSmoothCamera();
		}
		else if (e.hasRegularCamera) {
			e.AddSmoothCamera(e.regularCamera.offset)
				.AddVelocity(0.0f, 2.0f)
				.AddPosition(player.position.x, player.position.y + e.regularCamera.offset.y);
			e.RemoveRegularCamera();
		}
	}

	void changeTime() {
		TimeComponent component = _timeGroup.GetSingleEntity().time;
		component.modificator = Mathf.Abs(component.modificator - 1.0f) < EPSILON ? 0.2f : 1.0f;
	}
}