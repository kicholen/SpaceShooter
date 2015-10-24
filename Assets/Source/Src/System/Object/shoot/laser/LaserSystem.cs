using Entitas;
using UnityEngine;

public class LaserSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _group;
	const float pixelsPerUnit = 100.0f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Laser, Matcher.GameObject));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _group.GetEntities()) {
			LaserComponent component = e.laser;
			Entity source = component.source;
			if (source != null && source.hasLaserSpawner) { // todo make it child or sth
				PositionComponent sourcePosition = source.position;
				LaserSpawnerComponent sourceSpawner = source.laserSpawner;

				GameObject go = e.gameObject.gameObject;
				Transform transform = go.transform;
				transform.localScale = new Vector3(transform.localScale.x, sourceSpawner.height * pixelsPerUnit, transform.localScale.z);
				transform.localRotation = Quaternion.AngleAxis(sourceSpawner.angle, Vector3.forward);
				float xOffset = sourceSpawner.height / 2.0f * sourceSpawner.direction.x;
				float yOffset = sourceSpawner.height / 2.0f * sourceSpawner.direction.y;
				e.ReplacePosition(sourcePosition.x + xOffset, sourcePosition.y + yOffset);
			}
			else {
				e.isDestroyEntity = true;
			}
		}
	}
}