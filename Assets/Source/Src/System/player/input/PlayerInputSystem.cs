using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInputSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.Input.OnEntityAdded(); } }
	
	Pool _pool;
	Group _players;

	public void SetPool(Pool pool) {
		_pool = pool;
		_players = _pool.GetGroup(Matcher.Player);
	}
	
	public void Execute(List<Entity> entities) {
		Debug.Log("PlayerInputSystem");
		Entity e = entities.SingleEntity();
		modifyPlayerPosition(e.input);
	}
	
	void modifyPlayerPosition(InputComponent component) {
		foreach (var player in _players.GetEntities()) {
			setPosition(player, component);
		}
	}

	void setPosition(Entity entity, InputComponent component) {
		PositionComponent position = entity.position;
		VelocityComponent velocity = entity.velocity;

		velocity.x = (component.x - position.x);
		velocity.y = (component.y - position.y);
	}
}