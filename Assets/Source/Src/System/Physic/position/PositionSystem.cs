using Entitas;

public class PositionSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Velocity));
		_time = pool.GetGroup(Matcher.Time);
	}

	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _group.GetEntities()) {
			PositionComponent position = e.position;
			VelocityComponent velocity = e.velocity;

			e.ReplacePosition(position.x + velocity.x * deltaTime,
			                  position.y + velocity.y * deltaTime);
		}
	}
}
