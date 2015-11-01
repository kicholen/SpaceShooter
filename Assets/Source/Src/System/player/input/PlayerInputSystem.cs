using Entitas;
using UnityEngine;

public class PlayerInputSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _players;
	Group _slowGame;
	Group _camera;

	const float EPSILON = 0.005f;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = _pool.GetGroup(Matcher.Input); 
		_players = _pool.GetGroup(Matcher.Player);
		_camera = _pool.GetGroup(Matcher.Camera);
		_slowGame = _pool.GetGroup(Matcher.SlowGame);
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
				normalGameSpeed();
			}
			else {
				slowGameSpeed();
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
		Vector2 position = entity.position.pos;
		VelocityComponent velocity = entity.velocity;

		Entity cameraEntity = _camera.GetSingleEntity();
		float velocityOffsetY = cameraEntity.hasVelocity ? cameraEntity.velocity.vel.y : 0.0f;

		velocity.vel.Set((component.x - position.x) * 5.0f, (component.y - position.y) * 5.0f + velocityOffsetY);
	}

	void slowDown(Entity entity) {
		VelocityComponent velocity = entity.velocity;

		if (System.Math.Abs(velocity.vel.x) > EPSILON) {
			velocity.vel.x *= 0.8f;
		}
		else {
			velocity.vel.x = 0.0f;
		}

		if (System.Math.Abs(velocity.vel.y) > EPSILON) {
			velocity.vel.y *= 0.8f;
		}
		else {
			velocity.vel.y = 0.0f;
		}
	}

	void normalGameSpeed() {
		if (_slowGame.count > 0) {
			_slowGame.GetSingleEntity()
				.isDestroyEntity = true;
		}
	}

	void slowGameSpeed() {
		if (_slowGame.count == 0) {
			_pool.CreateEntity()
				.AddSlowGame(0.3f);
		}
	}
}