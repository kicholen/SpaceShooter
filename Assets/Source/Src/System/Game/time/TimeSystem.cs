using UnityEngine;
using Entitas;

public class TimeSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	Group _group;
	Pool _pool;

	public void Initialize() {
		_pool.CreateEntity()
			.AddTime(Time.deltaTime, Time.deltaTime, Time.time, 1.0f, true);
	}

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		Entity e = _group.GetSingleEntity();
		TimeComponent time = e.time;
		float deltaTime = Time.deltaTime;
		time.deltaTime = deltaTime;
		time.gameDeltaTime = deltaTime * time.modificator;
		time.time = Time.time;
	}
}
