using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class CreateCameraSystem : IInitializeSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateCamera.OnEntityAdded(); } }

	Pool _pool;
	Group _group;
	Group _player;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = _pool.GetGroup(Matcher.Camera);
		_player = _pool.GetGroup(Matcher.Player);
	}
	
	public void Initialize() {
		Camera camera = Camera.main;

		_pool.CreateEntity()
			.AddCamera(camera)
			.AddStaticCamera(getStartCameraPosition())
			.AddVelocity(new Vector2(0.0f, 0.0f))
			.AddPosition(new Vector2(0.0f, GameConfig.CAMERA_START_OFFSET));
	}

	public void Execute(List<Entity> entities) {
		Entity e = entities[0];
		e.isDestroyEntity = true;
		int type = e.createCamera.type;
		bool shouldReset = e.createCamera.shouldReset;

		Entity camera = _group.GetSingleEntity();

		if (type == CameraTypes.Smooth) {
			Entity player = _player.GetSingleEntity();
			Vector2 playerPosition = player.position.pos;
			camera.AddSmoothCamera(shouldReset ? getStartCameraPosition() : camera.staticCamera.offset)
				.ReplaceFollowTarget(player)
				.ReplaceVelocity(new Vector2(0.0f, GameConfig.CAMERA_SPEED))
				.ReplacePosition(new Vector2(playerPosition.x, playerPosition.y + camera.staticCamera.offset.y))
				.RemoveStaticCamera();
		}
		else if (type == CameraTypes.Static) {
			if (camera.hasSmoothCamera) {
				camera.AddStaticCamera(shouldReset ? getStartCameraPosition() : camera.smoothCamera.offset)
					.RemoveVelocity()
					.RemoveSmoothCamera();
				if (camera.hasShake) {
					camera.RemoveShake();
				}
			}
			else if (camera.hasDefaultCamera){
				camera.AddStaticCamera(shouldReset ? getStartCameraPosition() : camera.defaultCamera.offset)
					.RemoveVelocity()
					.RemoveDefaultCamera();
				if (camera.hasShake) {
					camera.RemoveShake();
				}
			}
		}
		else {
			camera.AddDefaultCamera(shouldReset ? getStartCameraPosition() : camera.staticCamera.offset)
				.RemoveVelocity()
				.RemoveStaticCamera();
		}
		
		resetCameraTransformIfNeeded(camera, shouldReset);
	}

	void resetCameraTransformIfNeeded(Entity e, bool shouldReset) {
		if (shouldReset) {
			e.camera.camera.transform.position = getStartCameraPosition();
		}
	}

	Vector3 getStartCameraPosition() {
		return new Vector3(0.0f, 0.0f, GameConfig.CAMERA_START_Z);
	}
}