using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class StartGameSystem : ClearGamePassiveSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.StartGame.OnEntityAdded(); } }
	
	Group _createLevels;
	Group _cameras;
	
	public void SetPool(Pool pool) {
		Init(pool);
		_createLevels = pool.GetGroup(Matcher.CreateLevel);
		_cameras = pool.GetGroup(Matcher.SmoothCamera);
	}
	
	public void Execute(List<Entity> entities) {
		int level = 0;
		foreach (Entity e in entities) {
			level = e.startGame.level;
			pool.DestroyEntity(e);
		}
		
		setCamera();
		restartLevel(level);
		clearGameObjects();
		clearEnemySpawners();
		clearHomeMissileSpawners();
		clearWaveSpawners();
		clearCameraShakes();
		clearGameStats();
        clearScore();

        createGrid();
		createShip();
		setInput(true);
	}
	
	void setCamera() {
		foreach (Entity e in _cameras.GetEntities()) {
			e.ReplacePosition(new Vector2(0.0f, 0.0f));
			if (e.hasSmoothCamera) {
				e.smoothCamera.offset = new Vector3(0.0f, 0.0f, 0.0f);
			}
			else if (e.hasDefaultCamera) {
				e.defaultCamera.offset = new Vector3(0.0f, 0.0f, 0.0f);
			}
			else if (e.hasStaticCamera) {
				e.staticCamera.offset = new Vector3(0.0f, 0.0f, 0.0f);
			}
		}
		pool.CreateEntity()
			.AddCreateCamera(CameraTypes.Smooth, false);
	}
	
	void restartLevel(int level) {
		foreach (Entity e in _createLevels.GetEntities()) {
			pool.DestroyEntity(e);
		}
		
		pool.CreateEntity()
			.AddCreateLevel(level, "/Resources/Content/level/");
	}

	void createGrid() {
		pool.CreateEntity()
			.IsCreateGrid(true);
	}

	void createShip() {
		pool.CreateEntity()
			.IsCreateShip(true);
	}
}
