using Entitas;
using UnityEngine;

public class CameraSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.Camera);
	}
	
	public void Execute() {
		Debug.Log("CameraSystem");
		foreach (Entity e in _group.GetEntities()) {
			updateCamera(e);
		}
	}
	
	void updateCamera(Entity e) {
		CameraComponent camera = e.camera;
		PositionComponent position = e.position;

		camera.camera.transform.position = new Vector3(position.x + camera.offset.x, position.y + camera.offset.y, camera.offset.z);
	}
}