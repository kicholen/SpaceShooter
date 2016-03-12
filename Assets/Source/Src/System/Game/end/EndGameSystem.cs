using Entitas;
using System.Collections.Generic;

public class EndGameSystem : ClearGamePassiveSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.EndGame.OnEntityAdded(); } }

	Group _createLevels;
	Group _players;
	Group _pause;
	Group _blockade;

	public void SetPool(Pool pool) {
		Init(pool);
		_createLevels = pool.GetGroup(Matcher.CreateLevel);
		_players = pool.GetGroup(Matcher.Player);
		_pause = pool.GetGroup(Matcher.PauseGame);
		_blockade = pool.GetGroup(Matcher.MovingBlockade);
	}
	
	public void Execute(List<Entity> entities) {
		entities[0].isDestroyEntity = true;
		setCamera();
		clearLevel();
		clearGameObjects();
		clearEnemySpawners();
		clearHomeMissileSpawners();
		clearWaveSpawners();
        clearMissileSpawners();
        clearCameraShakes();
		clearGameStats();
        clearScore();
        clearGrids();
		clearPlayer();
		clearPause();
		clearBlockade();
        clearShipBonus();
        setInput(false);
	}
	
	void setCamera() {
		pool.CreateEntity()
			.AddCreateCamera(CameraTypes.Static, true);
	}
	
	void clearLevel() {
		foreach(Entity e in _createLevels.GetEntities()) {
			pool.DestroyEntity(e);
		}
	}

	void clearPlayer() {
		foreach(Entity e in _players.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

	void clearPause() {
		foreach(Entity e in _pause.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

	void clearBlockade() {
		foreach(Entity e in _blockade.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}
}
