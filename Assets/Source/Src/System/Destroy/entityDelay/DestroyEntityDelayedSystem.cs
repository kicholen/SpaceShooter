using Entitas;

public class DestroyEntityDelayedSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_pool = pool;
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.DestroyEntityDelayed);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _group.GetEntities()) {
			DestroyEntityDelayedComponent component = e.destroyEntityDelayed;
			component.time -= deltaTime;
			if (component.time < 0.0f) {
				e.isDestroyEntity = true;
			}
		}
	}
}
