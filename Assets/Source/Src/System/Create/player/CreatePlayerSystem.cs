using UnityEngine;
using Entitas;

public class CreatePlayerSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		Debug.Log("CreatePlayerSystem");
		Camera camera = Camera.main;
		_pool.CreateEntity()
			.AddPlayer("superPlayer")
			.AddPosition(2.0f, 2.0f)
			.AddVelocity(0.0f, 0.0f)
			.AddAcceleration(0.0f, 0.0f)
			.AddCamera(camera, new Vector3(0.0f, 0.0f, camera.transform.position.z))
			.AddResource(Resource.Player);
	}
}