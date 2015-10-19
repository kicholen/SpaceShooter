using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.PauseGame.OnEntityAddedOrRemoved(); } }
	
	Pool _pool;
	Group _time;

	public void SetPool(Pool pool) {
		_pool = pool;
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute(List<Entity> entities) {
		Entity time = _time.GetSingleEntity();
		TimeComponent component = time.time;
		if (!component.isPaused) {
			component.modificator = 0.0f;
			component.isPaused = true;
		}
		else {
			component.isPaused = false;
			component.modificator =  1.0f;
		}
	}
}
