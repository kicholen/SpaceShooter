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
		Camera camera = Camera.main;
		Entity player = _group.GetSingleEntity();
		_pool.CreateEntity()
			.AddCamera(camera)
			.AddFollowTarget(player)
			.AddSmoothCamera(new Vector3(0.0f, 2.0f, camera.transform.position.z))
			.AddVelocity(0.0f, 2.0f)
			.AddPosition(player.position.x, player.position.y + 2.0f);
	}
}