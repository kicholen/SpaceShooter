using Entitas;
using System.Collections.Generic;

public class EndGameSystem : ClearGamePassiveSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.EndGame.OnEntityAdded(); } }

	Group _createLevels;
	Group _players;

	public void SetPool(Pool pool) {
		_pool = pool;
		base.SetPool(pool);
		_createLevels = pool.GetGroup(Matcher.CreateLevel);
		_players = pool.GetGroup(Matcher.Player);
	}
	
	public void Execute(List<Entity> entities) {
		entities[0].isDestroyEntity = true;
		setCamera();
		clearLevel();
		clearGameObjects();
		clearEnemySpawners();
		clearHomeMissileSpawners();
		clearWaveSpawners();
		clearCameraShakes();
		clearGameStats();
		clearPlayer();
		setInput(false);
	}
	
	void setCamera() {
		_pool.CreateEntity()
			.AddCreateCamera(CameraTypes.Static);
	}
	
	void clearLevel() {
		foreach(Entity e in _createLevels.GetEntities()) {
			_pool.DestroyEntity(e);
		}
	}

	void clearPlayer() {
		foreach(Entity e in _players.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}
}
