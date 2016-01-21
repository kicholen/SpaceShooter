using Entitas;
using System.Collections.Generic;

public class MovingBlockadeSystem : IExecuteSystem, ISetPool { // further development of TweenSystem == this system useless
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
				e.AddTween(true, new List<Tween>());
				TweenComponent tweenComponent = e.tween;
				tweenComponent.AddTween(position, EaseTypes.linear, PositionAccessorType.X, component.duration)
					.From(position.pos.x)
					.To(position.pos.x + (component.offset * component.direction))
					.SetEndCallback((ent) => {
							MovingBlockadeComponent cmp = ent.movingBlockade;
							cmp.direction = -cmp.direction;
							cmp.time = component.stopDuration;
					});
			}
		}
	}
}