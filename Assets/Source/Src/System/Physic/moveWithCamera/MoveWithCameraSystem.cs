using Entitas;
using UnityEngine;

public class MoveWithCameraSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _camera;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Velocity, Matcher.MoveWithCamera));
		_camera = pool.GetGroup(Matcher.AllOf(Matcher.Camera, Matcher.Velocity));
	}
	
	public void Execute() {
		if (_camera.count == 0) {
			return;
		}
		Vector2 cameraVelocity = _camera.GetSingleEntity().velocity.vel;
		foreach (Entity e in _group.GetEntities()) {
			VelocityComponent velocity = e.velocity;
			velocity.vel.y += cameraVelocity.y;
		}
	}
}
