using Entitas;
using UnityEngine;

public class PlayerInputSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _players;
	Group _timeGroup;
	bool hasChanged = true;
	const float EPSILON = 0.005f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = _pool.GetGroup(Matcher.Input); 
		_players = _pool.GetGroup(Matcher.Player);
		_timeGroup = _pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		Entity e = _group.GetSingleEntity();
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
		VelocityLimitComponent velocityLimit = entity.velocityLimit;

		/*float tx = (component.x - position.x);
		float ty = (component.y - position.y);
		float dist = Mathf.Sqrt(tx*tx+ty*ty);
		
		velocity.x = (tx/dist)*5.0f;
		velocity.y = (ty/dist)*5.0f;*/

		velocity.x = Mathf.Max(-(velocityLimit.x + velocityLimit.offsetX), Mathf.Min((component.x - position.x) * 5.0f, velocityLimit.x + velocityLimit.offsetX)); // todo sth is fucked up here
		velocity.y = Mathf.Max(-(velocityLimit.y + velocityLimit.offsetY), Mathf.Min((component.y - position.y) * 5.0f, velocityLimit.y + velocityLimit.offsetY));
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