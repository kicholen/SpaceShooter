using Entitas;
using System.Collections.Generic;

public class SpeedBonusSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _player;
	Group _time;
	Group _eventService;

	public void SetPool(Pool pool) {
		_pool = pool;
		_player = pool.GetGroup(Matcher.Player);
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.SpeedBonus);
		_eventService = pool.GetGroup(Matcher.EventService);
	}

	public void Execute() {
		if (_group.count == 0) {
			return;
		}
		Entity player = _player.GetSingleEntity();
		if (player != null) {
			float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
			VelocityLimitComponent limit = player.velocityLimit;

			foreach (Entity e in _group.GetEntities()) {
				SpeedBonusComponent component = e.speedBonus;
				if (!e.hasParent) {
					_eventService.GetSingleEntity().eventService.dispatcher.Dispatch<SpeedBonusEvent>(new SpeedBonusEvent(true));
					List<Entity> children = new List<Entity>();
					children.Add(_pool.CreateEntity().AddIndicator(0.0f, component.time, IndicatorTypes.SpeedIndicator));
					e.AddParent(children);
				}
				component.time -= deltaTime;
				e.parent.children[0].indicator.currentValue = component.time;
				if (component.time < 0.0f) {
					limit.maxVelocity = component.savedVelocity;
					_eventService.GetSingleEntity().eventService.dispatcher.Dispatch<SpeedBonusEvent>(new SpeedBonusEvent(false));
					e.isDestroyEntity = true;
				}
				else {
					limit.maxVelocity = component.velocity;
				}
			}
		}
	}
}