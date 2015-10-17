using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.PauseGame.OnEntityAddedOrRemoved(); } }
	
	Pool _pool;
	Group _time;

	const float EPSILON = 0.005f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute(List<Entity> entities) {
		Entity time = _time.GetSingleEntity();
		TimeComponent component = time.time;
		component.modificator = Mathf.Abs(component.modificator - 1.0f) < EPSILON ? 0.0f : 1.0f;
	}
}
