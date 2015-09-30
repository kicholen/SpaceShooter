using Entitas;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class CreateLevelSystem : IReactiveSystem, IInitializeSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateLevel.OnEntityAdded(); } }

	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}

	public void Initialize() {
		Debug.Log("CreateLevelSystem");
		_pool.CreateEntity()
			.AddCreateLevel(1, "/Resources/Content/level/"); // todo make another system for handling this
	}
	
	public void Execute(List<Entity> entities) {
		Debug.Log("CreateLevelSystem");
		for (int i = 0; i < entities.Count; i++) {
			Entity e = entities[i];
			CreateLevelComponent createLevel = e.createLevel;
			XmlNode node = loadXml(createLevel);

			_pool.CreateEntity()
				.AddEnemySpawner(createLevel.level, false, node.FirstChild);
		}
	}

	XmlNode loadXml(CreateLevelComponent component) {
		XmlDocument doc = new XmlDocument();
		doc.Load(Application.dataPath + component.path + component.level + ".xml");
		return doc.FirstChild;
	}
}