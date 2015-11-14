using Entitas;
using System.Collections.Generic;

public class EndGameSystem : ClearGamePassiveSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.EndGame.OnEntityAdded(); } }

	Group _createLevels;
	Group _players;
	Group _cameras;

	public void SetPool(Pool pool) {
		_pool = pool;
		base.SetPool(pool);
		_createLevels = pool.GetGroup(Matcher.CreateLevel);
		_players = pool.GetGroup(Matcher.Player);
		_cameras = pool.GetGroup(Matcher.SmoothCamera);
	}
	
	public void Execute(List<Entity> entities) {
		entities[0].isDestroyEntity = true;
		restartCamera();
		clearLevel();
		clearGameObjects();
		clearEnemySpawners();
		clearHomeMissileSpawners();
		clearWaveSpawners();
		clearBonuses();
		clearCameraShakes();
		clearGameStats();
		clearPlayer();
	}
	
	void restartCamera() {
		foreach(Entity e in _cameras.GetEntities()) {
			if (e.hasSmoothCamera) {
				e.AddStaticCamera(e.smoothCamera.offset)
				.RemoveVelocity()
				.RemoveSmoothCamera();
			}
		}
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
