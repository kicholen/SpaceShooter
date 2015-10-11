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
			_pool.DestroyEntity(e);
		}
	}
}
