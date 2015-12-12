using Entitas;
using UnityEngine;

public class ShakeSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

	const float TOTAL_ROAD_MULTIPLICATOR = 4.0f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Shake, Matcher.Position));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;

		foreach (Entity e in _group.GetEntities()) {
			ShakeProperties componentProperties = e.shake.properties;

			componentProperties.time -= deltaTime;
			if (componentProperties.time < 0.0f) {
				e.RemoveShake();
			}
			else {
				if (!componentProperties.velocityCalculated) {
					componentProperties.velocity = componentProperties.totalOffsetX * TOTAL_ROAD_MULTIPLICATOR / componentProperties.totalTime;
				}
				Vector2 originalPosition = e.position.pos;
				componentProperties.offsetX += componentProperties.direction * componentProperties.velocity * deltaTime;

				if (componentProperties.offsetX >= componentProperties.totalOffsetX) {
					componentProperties.direction = -1;
				}
				else if (componentProperties.offsetX <= -componentProperties.totalOffsetX) {
					componentProperties.direction = 1;
				}

				if (e.hasCamera) {
					e.camera.camera.transform.position = new Vector3(originalPosition.x + componentProperties.offsetX, originalPosition.y, Config.CAMERA_START_Z);
				}
				else {
					e.position.pos.Set(originalPosition.x + componentProperties.offsetX, originalPosition.y);
				}
			}
		}
	}
}