using Entitas;
using System.Collections.Generic;

public class SlowGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.SlowGame.OnEntityAddedOrRemoved(); } }
	
	Group _time;
	
	const float EPSILON = 0.005f;
	
	public void SetPool(Pool pool) {
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute(List<Entity> entities) {
		float value = 1.0f;
		if (entities[0].hasSlowGame) {
			value = entities[0].slowGame.value;
		}

		Entity time = _time.GetSingleEntity();
		TimeComponent component = time.time;
		if (!component.isPaused) {
			component.modificator = value;
		}
	}
}
