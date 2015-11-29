using Entitas;
using UnityEngine;

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

			if (component.time <= 0.0f && !e.hasTweenPosition) {
				component.time = component.duration;
				PositionComponent position = e.position;
				Vector2 to = new Vector2(position.pos.x + (component.offset * component.direction), position.pos.y);
				e.AddTweenPosition(0.0f, component.duration, EaseTypes.Linear, position.pos, to, (ent) => {
					MovingBlockadeComponent cmp = ent.movingBlockade;
					cmp.direction = -cmp.direction;
					cmp.time = component.stopDuration;
				}, null);
			}
		}
	}
}