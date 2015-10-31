using Entitas;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;

public class CreateLevelSystem : IReactiveSystem, IInitializeSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateLevel.OnEntityAdded(); } }

	Pool _pool;
	Group _players;
	Group _cameras;

	public void SetPool(Pool pool) {
		_pool = pool;
		_players = _pool.GetGroup(Matcher.Player);
		_cameras = _pool.GetGroup(Matcher.Camera);
	}

	public void Initialize() {
		_pool.CreateEntity()
			.AddCreateLevel(101, "/Resources/");
	}
	
	public void Execute(List<Entity> entities) {
		Entity e = entities.SingleEntity();

		CreateLevelComponent createLevel = e.createLevel;
		XmlNode node = Utils.LoadXml(createLevel.level.ToString());

		XmlNode enemyNode = node.FirstChild;
		_pool.CreateEntity()
			.AddEnemySpawner(createLevel.level, false, enemyNode.FirstChild);

		XmlNode sizeNode = enemyNode.NextSibling;
		XmlAttributeCollection attributes = sizeNode.Attributes;

		float x = (float)Convert.ToDouble(attributes[0].Value);
		float y = (float)Convert.ToDouble(attributes[1].Value);

		Entity cameraEntity = _cameras.GetSingleEntity();
		if (!cameraEntity.hasSnapPosition) {
			float width = (float)Convert.ToDouble (attributes [2].Value);
			float height = (float)Convert.ToDouble (attributes [3].Value);
			cameraEntity.AddSnapPosition(x, y, width, height, false);
		}

		Entity player = _players.GetSingleEntity();
		if (!player.hasSnapPosition) {
			Camera camera = cameraEntity.camera.camera;
			float screenWidth = camera.orthographicSize * camera.aspect * 2.0f;
			float screenHeight = camera.orthographicSize * 2.0f;
			player.AddSnapPosition(x, y, screenWidth, screenHeight, true);
		}

		createBonuses(sizeNode.NextSibling);
	}

	void createBonuses(XmlNode node) {
		if (node != null) {
			XmlNode bonus = node.FirstChild;

			while (bonus != null) {
				XmlAttributeCollection attributes = bonus.Attributes;
				_pool.CreateEntity()
					.AddBonusModel(Convert.ToInt16(attributes[0].Value), Convert.ToInt16(attributes[1].Value), Convert.ToInt16(attributes[2].Value), (float)Convert.ToDouble(attributes[3].Value), attributes[4].Value);

				bonus = bonus.NextSibling;
			}
		}
	}
}