using Entitas;
using UnityEngine;

public class PlayerInputSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	Pool pool;
	Group group;
	Group players;
	Group slowGame;

	public void SetPool(Pool pool) {
		this.pool = pool;
        group = pool.GetGroup(Matcher.Input);
        players = pool.GetGroup(Matcher.Player);
        slowGame = pool.GetGroup(Matcher.SlowGame);
	}

	public void Initialize() {
		pool.GetGroup(Matcher.EventService).GetSingleEntity().eventService.dispatcher.AddListener<GameActivateLaserEvent>(activateLaser);
		pool.GetGroup(Matcher.EventService).GetSingleEntity().eventService.dispatcher.AddListener<GameSpawnAtomBombEvent>(spawnAtomBomb);
	}

	public void Execute() {
		Entity e = group.GetSingleEntity();
		InputComponent input = e.input;
		updatePlayer(input);
	}
	
	void updatePlayer(InputComponent component) {
		if (component.isInputBlocked) {
			return;
		}
		bool isDown = component.isDown;
		foreach (Entity player in players.GetEntities()) {
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

		velocity.vel.Set((component.x - position.x) * 5.0f, (component.y - position.y) * 5.0f);
	}

	void slowDown(Entity entity) {
		VelocityComponent velocity = entity.velocity;
		velocity.vel.Set(0.0f, 0.0f);
	}

	void normalGameSpeed() {
		if (slowGame.count > 0) {
			slowGame.GetSingleEntity()
				.isDestroyEntity = true;
		}
	}

	void slowGameSpeed() {
		if (slowGame.count == 0) {
			pool.CreateEntity()
				.AddSlowGame(0.3f);
		}
	}

	void activateLaser(GameActivateLaserEvent e) {
		Entity player = players.GetSingleEntity();

		if (!player.hasLaserSpawner) {
			player.AddLaserSpawner(5.0f, 10.0f, 10.0f, 0.0f, new Vector2(), CollisionTypes.Player, 1, Resource.Laser, null)
				.AddDelayedCall(5.0f, deactivateLaser);
		}
    }

    void spawnAtomBomb(GameSpawnAtomBombEvent e)
    {
        pool.CreateEntity()
            .IsAtomBomb(true);
    }

    void deactivateLaser(Entity player) {
		if (player!=null&&player.hasLaserSpawner)
			player.RemoveLaserSpawner();
	}
}