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
		SmoothCameraComponent camera = e.smoothCamera;
		PositionComponent position = e.position;

		float lerp = 0.1f;
		Vector3 cameraPosition = camera.camera.transform.position;
		temp.x = cameraPosition.x + (position.x - cameraPosition.x) * lerp;
		temp.y = cameraPosition.y + (position.y - cameraPosition.y) * lerp;

		camera.camera.transform.position = new Vector3(temp.x, temp.y, camera.offset.z);
	}
}