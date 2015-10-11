using Entitas;

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
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		Entity player = _player.GetSingleEntity();
		VelocityLimitComponent limit = player.velocityLimit;

		foreach (Entity e in _group.GetEntities()) {
			SpeedBonusComponent component = e.speedBonus;
			component.time -= deltaTime;
			if (component.time < 0.0f) {
				limit.offsetX = 0.0f;
				limit.offsetY = 0.0f;
				_pool.DestroyEntity(e);
			}
			else {
				limit.offsetX = component.velocityX;
				limit.offsetY = component.velocityY;
			}
		}
	}
}