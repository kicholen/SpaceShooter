using UnityEngine;
using Entitas;

public class PositionSystem : IExecuteSystem, ISetPool {
	Group _group;
	float _deltaTime;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Velocity));
	}

	public void Execute() {
		Debug.Log("PositionSystem");
		_deltaTime = Time.deltaTime;
		foreach (Entity e in _group.GetEntities()) {
			PositionComponent position = e.position;
			VelocityComponent velocity = e.velocity;

			e.ReplacePosition(position.x + velocity.x * _deltaTime,
			                  position.y + velocity.y * _deltaTime);
		}
	}
}
