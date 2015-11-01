using UnityEngine;
using Entitas;

public class PositionGameObjectSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.GameObject));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			e.gameObject.gameObject.transform.position = new Vector2().Set(e.position.pos);
		}
	}
}
