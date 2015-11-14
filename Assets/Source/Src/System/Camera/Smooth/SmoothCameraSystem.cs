using Entitas;
using UnityEngine;

public class SmoothCameraSystem : IExecuteSystem, ISetPool {
	Group _group;
	Vector3 temp;

	const float LERP_FACTOR = 0.1f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Camera, Matcher.SmoothCamera));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			updateCamera(e);
		}
	}
	
	void updateCamera(Entity e) {
		SmoothCameraComponent smoothCamera = e.smoothCamera;
		CameraComponent camera = e.camera;
		FollowTargetComponent target = e.followTarget;
		Vector2 targetPosition = target.target.position.pos;

		Vector3 cameraPosition = camera.camera.transform.position;
		temp.x = cameraPosition.x + (targetPosition.x - cameraPosition.x) * LERP_FACTOR;

		Vector2 position = e.position.pos;
		snapCamera(e, position);
		e.position.pos.Set(position);
		camera.camera.transform.position = new Vector3(temp.x, position.y, smoothCamera.offset.z);
	}

	void snapCamera(Entity e, Vector2 position) {
		SnapPositionComponent snapPosition = e.snapPosition;
		
		if (temp.x < snapPosition.x) {
			temp.x = snapPosition.x;
		}
		else if (temp.x > (snapPosition.x + snapPosition.width)) {
			temp.x = snapPosition.x + snapPosition.width;
		}
		
		if (position.y > (snapPosition.height + snapPosition.y)) {
			position.y = snapPosition.height + snapPosition.y;
		}
		else if (position.y < snapPosition.y) {
			position.y = snapPosition.y;
		}
	}
}