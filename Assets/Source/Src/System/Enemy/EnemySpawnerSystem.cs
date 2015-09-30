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
		Camera camera = _camera.GetSingleEntity().smoothCamera.camera;
		Vector3 cameraPosition = camera.transform.position;
		foreach (Entity e in _group.GetEntities()) {
			spawnIfCan(e, cameraPosition);
		}
	}
	
	void spawnIfCan(Entity e, Vector3 position) {
		EnemySpawnerComponent enemySpawner = e.enemySpawner;
		XmlNode node = enemySpawner.node;

		if (enemySpawner.used) {
			node = node.NextSibling;
		}
		if (node != null) {
			XmlAttributeCollection attributes = node.Attributes;
			float spawnPosition = (float)Convert.ToDouble(attributes[0].Value);
			if (spawnPosition < position.y) {
				XmlNode innerNode = node.FirstChild;
				while(innerNode != null) {
					int enemyCount = Convert.ToInt16(innerNode.Attributes[0].Value);
					while (enemyCount != 0) {
						_pool.CreateEntity()
							.AddEnemy(0)
							.AddPosition(2.0f, spawnPosition)
							.AddVelocity(0.0f, 0.1f)
							.AddVelocityLimit(5.0f, 5.0f)
							.AddHealth(10)
							.AddCollision(CollisionTypes.Enemy)
							.AddResource(Resource.Enemy);
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