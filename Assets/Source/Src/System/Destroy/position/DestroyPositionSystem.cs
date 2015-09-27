using UnityEngine;
using Entitas;

public class DestroyPositionSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.DestroyPosition);
	}
	
	public void Execute() {
		Debug.Log("DestroyPositionSystem");
		foreach (var e in _group.GetEntities()) {
			e.RemovePosition();
			e.isDestroyPosition = false;
		}
	}
}
