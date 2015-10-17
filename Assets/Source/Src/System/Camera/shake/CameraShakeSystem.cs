using Entitas;
using UnityEngine;

public class CameraShakeSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _camera;
	Group _time;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.CameraShake);
		_time = pool.GetGroup(Matcher.Time);
		_camera = pool.GetGroup(Matcher.Camera);
	}
	
	public void Execute() { // if shake is in progress do nnot shake more
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		CameraComponent camera = _camera.GetSingleEntity().camera;
		bool wasFirstOne = false;
		foreach (Entity e in _group.GetEntities()) {
			if (wasFirstOne) {
				return;
			}

			CameraShakeComponent component = e.cameraShake;
			component.time -= deltaTime;
			if (component.time < 0.0f) { // todo maybe amplify depending on count and remove rest entities
				foreach (Entity entity in _group.GetEntities()) {
					entity.isDestroyEntity = true;
				}
				return;
			}

			Vector3 originalPosition = camera.camera.transform.position;
			if (component.offsetX < -component.originalOffsetX) {
				component.offsetX -= component.originalOffsetX * deltaTime; 
			}
			else {
				component.offsetX += component.originalOffsetX * deltaTime; 
			}
			
			originalPosition.x += component.offsetX;
			originalPosition.y += component.offsetY;
			camera.camera.transform.position = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);

			wasFirstOne = true;
		}
	}
}