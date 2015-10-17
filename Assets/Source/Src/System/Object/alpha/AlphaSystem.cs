using Entitas;
using UnityEngine;

public class AlphaSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Alpha, Matcher.GameObject));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach(Entity e in _group.GetEntities()) {
			AlphaComponent alpha = e.alpha;
			alpha.time -= deltaTime;
			GameObjectComponent gameobject = e.gameObject;

			SpriteRenderer renderer = gameobject.gameObject.GetComponent<SpriteRenderer>();
			Color color = renderer.color;
			renderer.color = new Color(color.r, color.g, color.b, alpha.time / alpha.deltaTime);
		}
	}
}