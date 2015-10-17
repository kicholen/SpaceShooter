using Entitas;

public class DestroyEntitySystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.DestroyEntity);
	}

	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			if (e.hasParent) {
				ParentComponent parentComponent = e.parent;
				for (int i = 0; i < parentComponent.children.Count; i++) {
					Entity child = parentComponent.children[i];
					if (child != null) {
						_pool.DestroyEntity(child);
					}
				}
			}
			_pool.DestroyEntity(e);
		}
	}
}
