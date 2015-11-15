using Entitas;
using System.Collections.Generic;

public class DeadPlayerSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.CollisionDeath, Matcher.Player).OnEntityAdded(); } }

	Pool _pool;
	Group _time;
	Group _input;
	Group _eventService;

	public void SetPool(Pool pool) {
		_pool = pool;
		_time = pool.GetGroup(Matcher.Time);
		_input = pool.GetGroup(Matcher.Input);
		_eventService = pool.GetGroup(Matcher.EventService);
	}

	public void Execute(List<Entity> entities) {
		TimeComponent time = _time.GetSingleEntity().time;
		time.modificator = 0.2f;
		InputComponent input = _input.GetSingleEntity().input;
		input.isInputBlocked = true;

		_pool.CreateEntity()
			.AddCreateCamera(CameraTypes.Static);

		_eventService.GetSingleEntity().eventService.dispatcher.Dispatch<GameEndedEvent>(new GameEndedEvent(true));
	}
}
