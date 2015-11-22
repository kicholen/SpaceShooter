using Entitas;
using UnityEngine;
using System.Xml;
using System;

public class EnemySpawnerSystem : IExecuteSystem, ISetPool {

	Group _group;
	Group _camera;
	Group _difficulty;
	Pool _pool;
	EnemyFactory _factory;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.EnemySpawner);
		_camera = pool.GetGroup(Matcher.Camera);
		_difficulty = pool.GetGroup(Matcher.DifficultyController);
		_factory = new EnemyFactory();
		_factory.SetPool(_pool, _pool.GetGroup(Matcher.PathModel));
	}
	
	public void Execute() {
		DifficultyControllerComponent difficulty = _difficulty.GetSingleEntity().difficultyController;
		Camera camera = _camera.GetSingleEntity().camera.camera;
		Vector3 cameraPosition = camera.transform.position;
		foreach (Entity e in _group.GetEntities()) {
			spawnIfCan(e, cameraPosition, difficulty);
		}
	}
	
	void spawnIfCan(Entity e, Vector3 cameraPosition, DifficultyControllerComponent difficulty) {
		EnemySpawnerComponent enemySpawner = e.enemySpawner;
		XmlNode node = enemySpawner.node;

		if (enemySpawner.used && node != null) {
			enemySpawner.node = enemySpawner.node.NextSibling;
			node = enemySpawner.node;
		}
		if (node != null) {
			enemySpawner.used = false;
			XmlAttributeCollection attributes = node.Attributes;
			float spawnBarrier = (float)Convert.ToDouble(attributes[0].Value);
			if (spawnBarrier < cameraPosition.y) {
				XmlNode innerNode = node.FirstChild;
				while(innerNode != null) {
					int enemyCount = Convert.ToInt16(innerNode.Attributes[0].Value);
					if (enemyCount == 1) {
						float speed = (float)Convert.ToDouble(innerNode.Attributes[1].Value);
						int type = Convert.ToInt16(innerNode.Attributes[2].Value);
						int health = Convert.ToInt16(innerNode.Attributes[3].Value) * (difficulty.hpBoostPercent + 100) / 100;
						int path = Convert.ToInt16(innerNode.Attributes[4].Value);

						_factory.CreateEnemyByType(type, new Vector2(0.0f, cameraPosition.y), health, difficulty.missileSpeedBoostPercent, path, speed);
					}
					else {
						float timeOffset = (float)Convert.ToDouble(innerNode.Attributes[1].Value);
						float speed = (float)Convert.ToDouble(innerNode.Attributes[2].Value);
						int type = Convert.ToInt16(innerNode.Attributes[3].Value);
						int health = Convert.ToInt16(innerNode.Attributes[4].Value) * (difficulty.hpBoostPercent + 100) / 100;
						int path = Convert.ToInt16(innerNode.Attributes[5].Value);

						_pool.CreateEntity()
							.AddWaveSpawner(enemyCount, type, timeOffset, 0.0f, speed, health, path);
					}
					innerNode = innerNode.NextSibling;
				}

				enemySpawner.used = true;
			}
		}
		else {
			// all enemies spawned
		}
	}
}