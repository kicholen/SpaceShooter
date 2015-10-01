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
		Debug.Log("CreateLevelSystem");
		_pool.CreateEntity()
			.AddCreateLevel(1, "/Resources/Content/level/"); // todo make another system for handling this
	}
	
	public void Execute(List<Entity> entities) {
		Debug.Log("CreateLevelSystem");
		Entity e = entities.SingleEntity();

		CreateLevelComponent createLevel = e.createLevel;
		XmlNode node = loadXml(createLevel);

		XmlNode enemyNode = node.FirstChild;
		_pool.CreateEntity()
			.AddEnemySpawner(createLevel.level, false, enemyNode.FirstChild);

		XmlNode sizeNode = enemyNode.NextSibling;
		XmlAttributeCollection attributes = sizeNode.Attributes;

		float x = (float)Convert.ToDouble(attributes[0].Value);
		float y = (float)Convert.ToDouble(attributes[1].Value);
		float width = (float)Convert.ToDouble(attributes[2].Value);
		float height = (float)Convert.ToDouble(attributes[3].Value);

		Entity cameraEntity = _cameras.GetSingleEntity();
		cameraEntity.AddSnapPosition(x, y, width, height, false);
		Camera camera = cameraEntity.camera.camera;//= 
		float screenWidth = camera.orthographicSize * camera.aspect * 2.0f;
		float screenHeight = camera.orthographicSize * 2.0f;// todo what if player resize/rotate, needs to update 
		_players.GetSingleEntity()
			.AddSnapPosition(x, y, screenWidth, screenHeight, true);
	}

	XmlNode loadXml(CreateLevelComponent component) {
		XmlDocument doc = new XmlDocument();
		doc.Load(Application.dataPath + component.path + component.level + ".xml");
		return doc.FirstChild;
	}
}