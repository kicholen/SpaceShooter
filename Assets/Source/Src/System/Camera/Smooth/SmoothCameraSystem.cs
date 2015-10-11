using Entitas;
using UnityEngine;

public class SmoothCameraSystem : IExecuteSystem, ISetPool {
	Group _group;
	Vector3 temp = new Vector3();

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.SmoothCamera);
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			updateCamera(e);
		}
	}
	
	void updateCamera(Entity e) {
		SmoothCameraComponent smoothCamera = e.smoothCamera;
		PositionComponent position = e.position;
		CameraComponent camera = e.camera;
		PositionComponent playerPosition = camera.follow.position;
		// todo would be good to move it above position system, the snapCamera to trash 
		float lerp = 0.1f;
		Vector3 cameraPosition = camera.camera.transform.position;
		temp.x = cameraPosition.x + (playerPosition.x - cameraPosition.x) * lerp;

		snapCamera(e, position);
		camera.camera.transform.position = new Vector3(temp.x, position.y, smoothCamera.offset.z);
	}

	void snapCamera(Entity e, PositionComponent position) {
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