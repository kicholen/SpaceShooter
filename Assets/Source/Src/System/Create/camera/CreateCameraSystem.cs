using Entitas;
using UnityEngine;

public class CreateCameraSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	Group _group;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = _pool.GetGroup(Matcher.Player);
	}
	
	public void Initialize() {
		Debug.Log("CreateCameraSystem");
		Camera camera = Camera.main;
		Entity player = _group.GetSingleEntity();
		_pool.CreateEntity()
			.AddCamera(player)
			.AddRegularCamera(camera, new Vector3(0.0f, 2.0f, camera.transform.position.z));
	}
}