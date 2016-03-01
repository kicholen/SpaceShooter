using Entitas;
using UnityEngine;

public class RemoveOutOfViewGOSystem : IExecuteSystem, ISetPool {
	Group cameraGroup;
	Group group;
	const float offset = 1.5f;

	public void SetPool(Pool pool) {
		cameraGroup = pool.GetGroup(Matcher.Camera);
		group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.GameObject));
	}
	
	public void Execute() {
		Entity cameraEntity = cameraGroup.GetSingleEntity();
		Camera camera = cameraEntity.camera.camera;
        Vector2 cameraPosition = cameraEntity.position.pos;
		float ortographicSize = camera.orthographicSize;
		float radiusPower = camera.aspect > 1.0f ? ortographicSize * camera.aspect + offset : ortographicSize + offset;

		foreach (Entity e in group.GetEntities()) {
			Vector2 position = e.position.pos;
			if (!e.isNonRemovable && Vector2.Distance(cameraPosition, position) > radiusPower)
				e.isDestroyEntity = true;
		}
	}
}