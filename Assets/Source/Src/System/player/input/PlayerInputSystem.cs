using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInputSystem : IReactiveSystem, ISetPool { // todo maybe change it to execute, it's called every frame anyway
	public TriggerOnEvent trigger { get { return Matcher.Input.OnEntityAdded(); } }
	
	Pool _pool;
	Group _players;
	const float EPSILON = 0.005f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_players = _pool.GetGroup(Matcher.Player);
	}
	
	public void Execute(List<Entity> entities) {
		Entity e = entities.SingleEntity();
		InputComponent input = e.input;
		updatePlayer(input);
	}
	
	void updatePlayer(InputComponent component) {
		bool isDown = component.isDown;
		foreach (Entity player in _players.GetEntities()) {
			if (isDown) {
				setVelocityByInput(player, component);
				setWeapon(player);
			}
			else {
				slowDown(player);
				removeWeapon(player);
			}
		}
	}

	void setWeapon(Entity player) {
		if (!player.isWeapon) {
			player.isWeapon = true;
		}
	}
	
	void removeWeapon(Entity player) {
		if (player.isWeapon) {
			player.isWeapon = false;
		}
	}

	void setVelocityByInput(Entity entity, InputComponent component) {
		PositionComponent position = entity.position;
		VelocityComponent velocity = entity.velocity;

		/*float tx = (component.x - position.x);
		float ty = (component.y - position.y);
		float dist = Mathf.Sqrt(tx*tx+ty*ty);
		
		velocity.x = (tx/dist)*5.0f;
		velocity.y = (ty/dist)*5.0f;*/

		velocity.x = Mathf.Max(-5.0f, Mathf.Min((component.x - position.x) * 5.0f, 5.0f));
		velocity.y = Mathf.Max(-5.0f, Mathf.Min((component.y - position.y) * 5.0f, 5.0f));
	}

	void slowDown(Entity entity) {
		VelocityComponent velocity = entity.velocity;
		//PlayerComponent player = entity.player; todo add sth like PlayerInputControllerComponent

		if (System.Math.Abs(velocity.x) > EPSILON) {
			velocity.x *= 0.8f;
		}
		else {
			velocity.x = 0.0f;
		}

		if (System.Math.Abs(velocity.y) > EPSILON) {
			velocity.y *= 0.8f;
		}
		else {
			velocity.y = 0.0f;
		}
	}
}