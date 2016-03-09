using Entitas;
using UnityEngine;

public class MoveWithCameraSystem : IExecuteSystem, ISetPool {
	Group group;
	Group camera;
	
	public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.AllOf(Matcher.Velocity, Matcher.MoveWithCamera));
		camera = pool.GetGroup(Matcher.AllOf(Matcher.Camera, Matcher.Velocity));
	}
	
	public void Execute() {
		if (camera.count == 0) {
			return;
		}
		Vector2 cameraVelocity = camera.GetSingleEntity().velocity.vel;
		foreach (Entity e in group.GetEntities()) {
			VelocityComponent velocity = e.velocity;
			velocity.vel.y += cameraVelocity.y;
		}
	}
}
