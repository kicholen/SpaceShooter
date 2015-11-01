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
		_factory.SetPool(_pool);
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
					Vector2 position = new Vector2((float)Convert.ToDouble(innerNode.Attributes[1].Value),
					                               (float)Convert.ToDouble(innerNode.Attributes[2].Value));
					float velocityX = (float)Convert.ToDouble(innerNode.Attributes[3].Value);
					float velocityY = (float)Convert.ToDouble(innerNode.Attributes[4].Value);
					int type = Convert.ToInt16(innerNode.Attributes[5].Value);
					int health = Convert.ToInt16(innerNode.Attributes[6].Value) * (difficulty.hpBoostPercent + 100) / 100;
					while (enemyCount != 0) {
						_factory.createEnemyByType(type, position, velocityX, velocityY, health, difficulty.missileSpeedBoostPercent);
						enemyCount--;
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