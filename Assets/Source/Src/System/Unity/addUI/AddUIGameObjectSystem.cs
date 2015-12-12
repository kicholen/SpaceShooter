using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddUIGameObjectSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.UIResource.OnEntityAdded(); } }
	
	Group _poolGroup;
	Group _canvas;

	public void SetPool(Pool pool) {
		_poolGroup = pool.GetGroup(Matcher.PoolableGO);
		_canvas = pool.GetGroup(Matcher.Canvas);
	}
	
	public void Execute(List<Entity> entities) {
		Canvas canvas = _canvas.GetSingleEntity().canvas.canvas;
		for (int i = 0; i < entities.Count; i++) {
			Entity e = entities[i];
			string resourceName = e.uIResource.name;
			GameObject gameObject = getFromPool(resourceName);
			if (gameObject == null) {
				GameObject res = Resources.Load<GameObject>("Prefab/UI/Game/" + resourceName);
				try {
					gameObject = UnityEngine.Object.Instantiate(res);
				}
				catch (Exception) {
					Debug.Log("Cannot instantiate " + resourceName);
				}
			}
			
			gameObject.transform.SetParent(canvas.transform, false);
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
