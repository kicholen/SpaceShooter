using Entitas;

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
		PositionComponent position = entity.position;
		VelocityComponent velocity = entity.velocity;
		VelocityLimitComponent velocityLimit = entity.velocityLimit;

		/*float tx = (component.x - position.x);
		float ty = (component.y - position.y);
		float dist = Mathf.Sqrt(tx*tx+ty*ty);
		
		velocity.x = (tx/dist)*5.0f;
		velocity.y = (ty/dist)*5.0f;*/

		VelocityComponent cameraVelocity = _camera.GetSingleEntity().velocity;

		velocityLimit.offsetY = cameraVelocity.y;
		velocity.x = (component.x - position.x) * 5.0f;
		velocity.y = (component.y - position.y) * 5.0f + cameraVelocity.y;
		if (velocity.y < -velocityLimit.y) {
			velocity.y = -velocityLimit.y; // not to add new fields to velocityLimits
		}
	}

	void slowDown(Entity entity) {
		VelocityComponent velocity = entity.velocity;

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