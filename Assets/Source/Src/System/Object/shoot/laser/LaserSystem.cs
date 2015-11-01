using Entitas;
using UnityEngine;

public class LaserSystem : IExecuteSystem, ISetPool {
	Group _group;

	const float pixelsPerUnit = 100.0f;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Laser, Matcher.GameObject));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			LaserComponent component = e.laser;
			Entity source = component.source;
			if (source != null && source.hasLaserSpawner) { // todo make it child or sth
				Vector2 sourcePosition = source.position.pos;
				LaserSpawnerComponent sourceSpawner = source.laserSpawner;

				GameObject go = e.gameObject.gameObject;
				Transform transform = go.transform;
				transform.localScale = new Vector3(transform.localScale.x, sourceSpawner.height * pixelsPerUnit, transform.localScale.z);
				transform.localRotation = Quaternion.AngleAxis(sourceSpawner.angle, Vector3.forward);
				float xOffset = sourceSpawner.height / 2.0f * sourceSpawner.direction.x;
				float yOffset = sourceSpawner.height / 2.0f * sourceSpawner.direction.y;
				e.position.pos.Set(sourcePosition.x + xOffset, sourcePosition.y + yOffset);
			}
			else {
				e.isDestroyEntity = true;
			}
		}
	}
}