using Entitas;
using UnityEngine;

public class PositionSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Velocity));
		_time = pool.GetGroup(Matcher.Time);
	}

	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _group.GetEntities()) {
			Vector2 velocity = e.velocity.vel;
			velocity.Set(velocity.x * deltaTime, velocity.y * deltaTime);
			e.position.pos += velocity;
		}
	}
}
