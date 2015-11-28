using Entitas;
using System.Collections.Generic;

public class SpeedBonusSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _player;
	Group _time;

	public void SetPool(Pool pool) {
		_pool = pool;
		_player = pool.GetGroup(Matcher.Player);
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.SpeedBonus);
	}

	public void Execute() { // todo: can cause speed bugs 
		Entity player = _player.GetSingleEntity();
		if (player != null) {
			float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
			VelocityLimitComponent limit = player.velocityLimit;

			foreach (Entity e in _group.GetEntities()) {
				SpeedBonusComponent component = e.speedBonus;
				if (!e.hasParent) {
					List<Entity> children = new List<Entity>();
					children.Add(_pool.CreateEntity().AddIndicator(0.0f, component.time, IndicatorTypes.SpeedIndicator));
					e.AddParent(children);
				}
				component.time -= deltaTime;
				e.parent.children[0].indicator.currentValue = component.time;
				if (component.time < 0.0f) {
					limit.maxVelocity = component.savedVelocity;
					e.isDestroyEntity = true;
				}
				else {
					limit.maxVelocity = component.velocity;
				}
			}
		}
	}
}