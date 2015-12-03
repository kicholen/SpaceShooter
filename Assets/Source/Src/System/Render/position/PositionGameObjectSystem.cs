using Entitas;
using UnityEngine;

public class PositionGameObjectSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.GameObject));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			PositionComponent position = e.position;
			Transform transform = e.gameObject.gameObject.transform;
			transform.position = new Vector3(position.pos.x, position.pos.y, transform.position.z);
		}
	}
}
