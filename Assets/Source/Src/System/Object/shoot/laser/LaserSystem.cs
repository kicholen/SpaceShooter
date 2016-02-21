using Entitas;
using UnityEngine;

public class LaserSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Laser, Matcher.GameObject));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			LaserComponent component = e.laser;
			Entity source = component.source;
			if (source != null && source.hasLaserSpawner) {
				Vector2 sourcePosition = source.position.pos;
				LaserSpawnerComponent sourceSpawner = source.laserSpawner;

				GameObject go = e.gameObject.gameObject;
				LineRenderer lineRenderer = go.GetComponent<LineRenderer>();
				lineRenderer.SetPosition(1, new Vector3(sourcePosition.x, sourcePosition.y));
				lineRenderer.SetPosition(0, rotateAroundPivot(new Vector3(sourcePosition.x, sourcePosition.y + sourceSpawner.height), sourcePosition, sourceSpawner.angle));
				go.transform.rotation = Quaternion.AngleAxis(sourceSpawner.angle, Vector3.forward);
				e.position.pos.Set(sourcePosition.x, sourcePosition.y);
			}
			else {
				e.isDestroyEntity = true;
			}
		}
	}

	Vector3 rotateAroundPivot(Vector3 point, Vector3 pivot, float angle) {
		Vector3 direction = point - pivot;
		direction = Quaternion.Euler(0.0f, 0.0f, angle) * direction;
		point = direction + pivot;
		return point;
	}
}