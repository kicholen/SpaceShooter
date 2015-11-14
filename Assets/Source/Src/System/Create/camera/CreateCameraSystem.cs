using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class CreateCameraSystem : IInitializeSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateCamera.OnEntityAdded(); } }

	Pool _pool;
	Group _group;
	Group _player;
	const float CAMERA_SPEED = 1.0f;
	const float CAMERA_START_OFFSET = 2.0f;
	const float CAMERA_START_Z = -10.0f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = _pool.GetGroup(Matcher.Camera);
		_player = _pool.GetGroup(Matcher.Player);
	}
	
	public void Initialize() {
		Camera camera = Camera.main;

		_pool.CreateEntity()
			.AddCamera(camera)
			.AddStaticCamera(new Vector3(0.0f, 0.0f, CAMERA_START_Z))
			.AddVelocity(new Vector2(0.0f, 0.0f))
			.AddPosition(new Vector2(0.0f, CAMERA_START_OFFSET));
	}

	public void Execute(List<Entity> entities) {
		Entity e = entities[0];
		e.isDestroyEntity = true;
		int type = e.createCamera.type;
		Entity camera = _group.GetSingleEntity();

		if (type == CameraTypes.Smooth) {
			Entity player = _player.GetSingleEntity();
			Vector2 playerPosition = player.position.pos;
			camera.AddSmoothCamera(camera.staticCamera.offset)
				.ReplaceFollowTarget(player)
				.ReplaceVelocity(new Vector2(0.0f, CAMERA_SPEED))
				.ReplacePosition(new Vector2(playerPosition.x, playerPosition.y + camera.staticCamera.offset.y))
				.RemoveStaticCamera();
		}
		else {
			camera.AddRegularCamera(e.staticCamera.offset)
				.RemoveVelocity()
				.RemoveStaticCamera();
		}
	}
}