using Entitas;

public class MovingBlockadeSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.MovingBlockade, Matcher.Position));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach(Entity e in _group.GetEntities()) {
			MovingBlockadeComponent component = e.movingBlockade;
			component.time -= deltaTime;

			if (component.time <= 0.0f && !e.hasTween) {
				component.time = component.duration;
				PositionComponent position = e.position;
				e.AddTween(0.0f, component.duration, EaseTypes.Linear, position.pos.x, position.pos.x + (component.offset * component.direction), 0.0f, false, (ent, value) => {
					PositionComponent pos = ent.position;
					pos.pos.x = value;
				}, (ent) => {
					ent.RemoveTween();
					MovingBlockadeComponent cmp = ent.movingBlockade;
					cmp.direction = -cmp.direction;
					cmp.time = component.stopDuration;
				});
			}
		}
	}
}