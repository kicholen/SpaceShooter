using Entitas;
using UnityEngine;

public class ActiveSystem : IExecuteSystem, ISetPool {
	Group _camera;
	Group _group;
	
	public void SetPool(Pool pool) {
		_camera = pool.GetGroup(Matcher.Camera);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.NonRemovable, Matcher.Active));
	}
	
	public void Execute() {
		Rect rect = getCameraRect();
		foreach (Entity e in _group.GetEntities()) {
			if (rect.Contains(e.position.pos)) {
				e.isNonRemovable = false;
				e.isActive = false;
			}
		}
	}

	Rect getCameraRect() {
		Entity cameraEntity = _camera.GetSingleEntity();
		Camera camera = cameraEntity.camera.camera;
		float width = 0.0f;
		float height = 0.0f;
		Vector3 position = camera.transform.position;
		float size = camera.orthographicSize;
		float aspect = camera.aspect;
		if (camera.aspect < 1.0f) {
			height = size;
			width = size * aspect;
		}
		else {
			width = size;
			height = size * aspect;
		}

		return new Rect(position.x - width / 2.0f, position.y - height / 2.0f, width, height);
	}
}
