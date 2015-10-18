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

			CameraShakeProperties componentProperties = e.cameraShake.properties;

			componentProperties.time -= deltaTime;
			if (componentProperties.time < 0.0f) {
				e.isDestroyEntity = true;
			}
			else {
				if (!componentProperties.velocityCalculated) {
					componentProperties.velocity = componentProperties.totalOffsetX * 4.0f / componentProperties.totalTime;
				}
				Vector3 originalPosition = camera.camera.transform.position;
				componentProperties.offsetX += componentProperties.direction * componentProperties.velocity * deltaTime;

				if (componentProperties.offsetX >= componentProperties.totalOffsetX) {
					componentProperties.direction = -1;
				}
				else if (componentProperties.offsetX <= -componentProperties.totalOffsetX) {
					componentProperties.direction = 1;
				}

				camera.camera.transform.position = new Vector3(originalPosition.x + componentProperties.offsetX, originalPosition.y, originalPosition.z);
				wasFirstOne = true;
			}
		}
	}
}