using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddGameObjectSystem : IReactiveSystem, ISetPool {
    public TriggerOnEvent trigger { get { return Matcher.Resource.OnEntityAdded(); } }

    readonly Transform _viewContainer = new GameObject("Views").transform;

	Group _poolGroup;

	public void SetPool(Pool pool) {
		_poolGroup = pool.GetGroup(Matcher.PoolableGO);
	}

    public void Execute(List<Entity> entities) {
		Debug.Log("AddGameObjectSystem");
        for (int i = 0; i < entities.Count; i++) {
			Entity e = entities[i];
			string resourceName = e.resource.name;
			GameObject gameObject = getFromPool(resourceName);
			if (gameObject == null) {
				GameObject res = Resources.Load<GameObject>("Prefab/" + resourceName);
				try {
					gameObject = UnityEngine.Object.Instantiate (res);
				}
				catch (Exception) {
					Debug.Log("Cannot instantiate " + res);
				}
			}
			if (e.hasCollision) {
				gameObject.AddComponent<CollisionScript>();
			}

			gameObject.transform.SetParent (_viewContainer, false);
			e.AddGameObject(gameObject, resourceName);
		}
    }

	GameObject getFromPool(string name) {
		foreach (Entity e in _poolGroup.GetEntities()) {
			PoolableGOComponent poolGo = e.poolableGO;
			if (poolGo.name.Equals(name) && poolGo.queue.Count > 0) {
				GameObject gameObject = poolGo.queue.Dequeue();
				gameObject.SetActive(true);
				return gameObject;
			}
		}
		return null;
	}
}
