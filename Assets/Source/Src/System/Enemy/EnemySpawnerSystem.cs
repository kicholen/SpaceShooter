using Entitas;
using UnityEngine;
using System.Xml;
using System;

public class EnemySpawnerSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _camera;
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.EnemySpawner);
		_camera = pool.GetGroup(Matcher.SmoothCamera);
	}
	
	public void Execute() {
		#if (UNITY_EDITOR)
		if (_camera.GetEntities().Length != 1) {
			return;
		}
		#endif

		Debug.Log("EnemySpawnerSystem");
		Camera camera = _camera.GetSingleEntity().camera.camera;
		Vector3 cameraPosition = camera.transform.position;
		foreach (Entity e in _group.GetEntities()) {
			spawnIfCan(e, cameraPosition);
		}
	}
	
	void spawnIfCan(Entity e, Vector3 position) {
		EnemySpawnerComponent enemySpawner = e.enemySpawner;
		XmlNode node = enemySpawner.node;

		if (enemySpawner.used) {
			enemySpawner.node = enemySpawner.node.NextSibling;
			node = enemySpawner.node;
		}
		if (node != null) {
			enemySpawner.used = false;
			XmlAttributeCollection attributes = node.Attributes;
			float spawnBarrier = (float)Convert.ToDouble(attributes[0].Value);
			if (spawnBarrier < position.y) {
				XmlNode innerNode = node.FirstChild;
				while(innerNode != null) {
					int enemyCount = Convert.ToInt16(innerNode.Attributes[0].Value);
					float x = (float)Convert.ToDouble(innerNode.Attributes[1].Value);
					float y = (float)Convert.ToDouble(innerNode.Attributes[2].Value);
					float velocityX = (float)Convert.ToDouble(innerNode.Attributes[3].Value);
					float velocityY = (float)Convert.ToDouble(innerNode.Attributes[4].Value);
					int type = Convert.ToInt16(innerNode.Attributes[5].Value);
					int health = Convert.ToInt16(innerNode.Attributes[6].Value);
					while (enemyCount != 0) {
						_pool.CreateEntity()
							.AddEnemy(type)
							.AddPosition(x, y)
							.AddVelocity(velocityX, velocityY)
							.AddVelocityLimit(5.0f, 5.0f)
							.AddHealth(health)
							.AddCollision(CollisionTypes.Enemy)
							.AddResource(Resource.Enemy)
							.isFaceDirection = true;
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