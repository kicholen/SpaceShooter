using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.RestartGame.OnEntityAdded(); } }
	
	Pool _pool;
	Group _createLevels;
	Group _players;
	Group _cameras;
	Group _resources;
	Group _enemySpawners;
	Group _bonusSpawners;
	Group _homeMissileSpawners;
	Group _bonuses;

	public void SetPool(Pool pool) {
		_pool = pool;
		_createLevels = pool.GetGroup(Matcher.CreateLevel);
		_players = pool.GetGroup(Matcher.Player);
		_cameras = pool.GetGroup(Matcher.SmoothCamera);
		_resources = pool.GetGroup(Matcher.Resource);
		_enemySpawners = pool.GetGroup(Matcher.EnemySpawner);
		_homeMissileSpawners = pool.GetGroup(Matcher.HomeMissileSpawner);
		_bonuses = pool.GetGroup(Matcher.BonusModel);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			_pool.DestroyEntity(e);
		}

		restartCamera();
		restartLevel();
		clearGameObjects();
		clearEnemySpawners();
		clearHomeMissileSpawners();
		clearBonuses();

		restartPlayer();
	}

	void restartPlayer() {
		Entity player = _players.GetSingleEntity();
		player.ReplacePosition(0.0f, 0.0f);
		player.ReplaceHealth(50);
		player.isDestroyEntity = false;
		if (player.hasParent) {
			List<Entity> children = player.parent.children;
			for (int i = 0; i < children.Count; i++) {
				children[i].isDestroyEntity = false;
			}
		}
	}

	void restartCamera() {
		foreach (Entity e in _cameras.GetEntities()) {
			e.ReplacePosition(0.0f, 0.0f);
		}
	}

	void restartLevel() {
		foreach (Entity e in _createLevels.GetEntities()) {
			_pool.DestroyEntity(e);
		}

		_pool.CreateEntity()
			.AddCreateLevel(1, "/Resources/Content/level/")
			.isDestroyEntity = true;
	}

	void clearEnemySpawners() {
		foreach (Entity e in _enemySpawners.GetEntities()) {
			_pool.DestroyEntity(e);
		}
	}

	void clearHomeMissileSpawners() {
		foreach (Entity e in _homeMissileSpawners.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

	void clearGameObjects() {
		foreach (Entity e in _resources.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

	void clearBonuses() {
		foreach (Entity e in _bonuses.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}
}
