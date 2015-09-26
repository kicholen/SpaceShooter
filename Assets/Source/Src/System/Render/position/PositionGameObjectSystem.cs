using UnityEngine;
using Entitas;

public class PositionGameObjectSystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position));
	}
	
	public void Execute() {
		Debug.Log("PositionSystem");
		foreach (Entity e in _group.GetEntities()) {
			PositionComponent position = e.position;
			e.gameObject.gameObject.transform.position = new Vector3(position.x, position.y, 0);
		}
	}
}
