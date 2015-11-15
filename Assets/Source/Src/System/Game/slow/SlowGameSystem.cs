using Entitas;
using System.Collections.Generic;

public class SlowGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.SlowGame.OnEntityAddedOrRemoved(); } }
	
	Group time;
	Group eventService;

	const float EPSILON = 0.005f;
	
	public void SetPool(Pool pool) {
		time = pool.GetGroup(Matcher.Time);
		eventService = pool.GetGroup(Matcher.EventService);
	}
	
	public void Execute(List<Entity> entities) {
		float value = 1.0f;
		if (entities[0].hasSlowGame) {
			value = entities[0].slowGame.value;
		}

		Entity e = time.GetSingleEntity();
		TimeComponent component = e.time;
		if (!component.isPaused) {
			component.modificator = value;
			eventService.GetSingleEntity().eventService.dispatcher.Dispatch<GameSlowEvent>(new GameSlowEvent(value));
		}
	}
}
