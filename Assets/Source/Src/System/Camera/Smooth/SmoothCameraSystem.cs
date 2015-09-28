using Entitas;
using UnityEngine;

public class SmoothCameraSystem : IExecuteSystem, ISetPool {
	Group _group;
	Vector3 temp = new Vector3();

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.SmoothCamera);
	}
	
	public void Execute() {
		Debug.Log("SmoothCameraSystem");
		foreach (Entity e in _group.GetEntities()) {
			updateCamera(e);
		}
	}
	
	void updateCamera(Entity e) {
		SmoothCameraComponent smoothCamera = e.smoothCamera;
		PositionComponent position = e.position;
		CameraComponent camera = e.camera;
		PositionComponent playerPosition = camera.follow.position;

		float lerp = 0.1f;
		Vector3 cameraPosition = smoothCamera.camera.transform.position;
		temp.x = cameraPosition.x + (playerPosition.x - cameraPosition.x) * lerp;

		smoothCamera.camera.transform.position = new Vector3(temp.x, position.y, smoothCamera.offset.z);
	}
}