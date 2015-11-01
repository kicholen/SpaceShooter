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
		Vector2 playerPosition = player.position.pos;
		_pool.CreateEntity()
			.AddCamera(camera)
			.AddFollowTarget(player)
			.AddSmoothCamera(new Vector3(0.0f, 2.0f, camera.transform.position.z))
			.AddVelocity(new Vector2(0.0f, 2.0f))
			.AddPosition(new Vector2(playerPosition.x, playerPosition.y + 2.0f));
	}
}