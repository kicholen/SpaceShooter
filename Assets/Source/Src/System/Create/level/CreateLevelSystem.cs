using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class CreateLevelSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateLevel.OnEntityAdded(); } }

	Pool _pool;
	Group _group;
	Group _players;
	Group _cameras;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = _pool.GetGroup(Matcher.LevelModel);
		_players = _pool.GetGroup(Matcher.Player);
		_cameras = _pool.GetGroup(Matcher.Camera);
	}
	
	public void Execute(List<Entity> entities) {
		Entity e = entities.SingleEntity();
		e.isDestroyEntity = true;

		CreateLevelComponent createLevel = e.createLevel;

		LevelModelComponent component = getLevelModelIfExists(createLevel.level.ToString());
		if (component == null) {
			component = Utils.Deserialize<LevelModelComponent>(createLevel.level.ToString());
			_pool.CreateEntity()
				.AddComponent(ComponentIds.LevelModel, component);
		}
		else {
			component.enemyIndex = 0;
			component.waveIndex = 0;
		}

		_pool.CreateEntity()
			.AddEnemySpawner(createLevel.level, false, component);

		Entity cameraEntity = _cameras.GetSingleEntity();
		if (!cameraEntity.hasSnapPosition) {
			cameraEntity.AddSnapPosition(component.position.x, component.position.y, component.size.x, component.size.y, false);
		}

		Entity player = _players.GetSingleEntity();
		if (!player.hasSnapPosition) {
			Camera camera = cameraEntity.camera.camera;
			float screenWidth = camera.orthographicSize * camera.aspect * 2.0f;
			float screenHeight = camera.orthographicSize * 2.0f;
			player.AddSnapPosition(component.position.x, component.position.y, screenWidth, screenHeight, true);
		}
	}

	LevelModelComponent getLevelModelIfExists(string name) {
		foreach (Entity e in _group.GetEntities()) {
			if (name == e.levelModel.name) {
				return e.levelModel;
			}
		}
		return null;
	}
}