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
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		CameraComponent camera = _camera.GetSingleEntity().camera;
		bool wasFirstOne = false;
		foreach (Entity e in _group.GetEntities()) {
			if (wasFirstOne) {
				e.isDestroyEntity = true;
				return;
			}

			CameraShakeComponent component = e.cameraShake;
			component.time -= deltaTime;
			if (component.time < 0.0f) { // todo maybe amplify depending on count and remove rest entities
				e.isDestroyEntity = true;
			}
			else {
				if (!component.velocityCalculated) {
					component.velocity = component.totalOffsetX * 4.0f / component.totalTime;
				}
				Vector3 originalPosition = camera.camera.transform.position;
				component.offsetX += component.direction * component.velocity * deltaTime;

				if (component.offsetX >= component.totalOffsetX) {
					component.direction = -1;
				}
				else if (component.offsetX <= -component.totalOffsetX) {
					component.direction = 1;
				}

				camera.camera.transform.position = new Vector3(originalPosition.x + component.offsetX, originalPosition.y, originalPosition.z);
				wasFirstOne = true;
			}
		}
	}
}