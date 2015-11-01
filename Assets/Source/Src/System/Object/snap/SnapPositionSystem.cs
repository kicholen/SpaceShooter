using Entitas;
using UnityEngine;

public class SnapPositionSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _camera;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.SnapPosition, Matcher.Position));
		_camera = pool.GetGroup(Matcher.Camera);
	}
	
	public void Execute() {
		Camera camera = _camera.GetSingleEntity().camera.camera;

		foreach (Entity e in _group.GetEntities()) {
			PositionComponent positionComponent = e.position;
			Vector2 position = e.position.pos;
			SnapPositionComponent snapPosition = e.snapPosition;

			if (snapPosition.shouldSnapToCameraY) {
				snapPosition.y = camera.transform.position.y - camera.orthographicSize;
				snapPosition.x = camera.transform.position.x - camera.orthographicSize * camera.aspect;
			}

			if (position.x < snapPosition.x) {
				positionComponent.pos.x = snapPosition.x;
			}
			else if (position.x > (snapPosition.x + snapPosition.width)) {
				positionComponent.pos.x = snapPosition.x + snapPosition.width;
			}
			
			if (position.y > (snapPosition.height + snapPosition.y)) {
				positionComponent.pos.y = snapPosition.height + snapPosition.y;
			}
			else if (position.y < snapPosition.y) {
				positionComponent.pos.y = snapPosition.y;
			}
		}
	}
}