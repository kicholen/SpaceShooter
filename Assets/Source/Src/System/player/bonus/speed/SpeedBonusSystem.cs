using Entitas;

public class SpeedBonusSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _player;
	Group _time;

	public void SetPool(Pool pool) {
		_player = pool.GetGroup(Matcher.Player);
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.SpeedBonus);
	}
	
	public void Execute() { // todo: can cause speed bugs 
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		Entity player = _player.GetSingleEntity();
		VelocityLimitComponent limit = player.velocityLimit;

		foreach (Entity e in _group.GetEntities()) {
			SpeedBonusComponent component = e.speedBonus;
			component.time -= deltaTime;
			if (component.time < 0.0f) {
				limit.maxVelocity = component.savedVelocity;
				e.isDestroyEntity = true;
			}
			else {
				component.savedVelocity = limit.maxVelocity;
				limit.maxVelocity = component.velocity;
			}
		}
	}
}