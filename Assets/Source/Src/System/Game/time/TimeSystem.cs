using UnityEngine;
using Entitas;

public class TimeSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	Group _group;
	Pool _pool;

	public void Initialize() {
		_pool.CreateEntity()
			.AddTime(Time.deltaTime, Time.time, 1.0f, false);
	}

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		Entity e = _group.GetSingleEntity();
		TimeComponent time = e.time;
		time.deltaTime = Time.deltaTime * time.modificator;
		time.time = Time.time;
	}
}
