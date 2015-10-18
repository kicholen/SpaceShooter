using Entitas;
using UnityEngine;

public class RegularCameraSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.RegularCamera);
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			updateCamera(e);
		}
	}
	
	void updateCamera(Entity e) {
		RegularCameraComponent regularCamera = e.regularCamera;
		CameraComponent camera = e.camera;
		FollowTargetComponent target = e.followTarget;
		PositionComponent targetPosition = target.target.position;

		camera.camera.transform.position = new Vector3(targetPosition.x + regularCamera.offset.x, targetPosition.y + regularCamera.offset.y, regularCamera.offset.z);
	}
}