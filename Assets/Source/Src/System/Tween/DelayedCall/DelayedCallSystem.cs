using Entitas;

public class DelayedCallSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.DelayedCall);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		
		foreach (Entity e in _group.GetEntities()) {
			DelayedCallComponent component = e.delayedCall;
			component.duration -= deltaTime;
			if (component.duration <= 0.0f) {
				component.onComplete(e);
				e.RemoveDelayedCall();
			}
		}
	}
}
