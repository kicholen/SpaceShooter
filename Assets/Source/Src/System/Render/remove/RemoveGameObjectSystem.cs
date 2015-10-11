using UnityEngine;
using Entitas;
using System.Collections.Generic;
using System;

public class RemoveGameObjectSystem : IReactiveSystem, ISetPool, IEnsureComponents {
	public TriggerOnEvent trigger { get { return Matcher.Resource.OnEntityRemoved(); } }
	
	public IMatcher ensureComponents { get { return Matcher.GameObject; } }

	Group _poolGroup;
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
		pool.GetGroup(Matcher.GameObject).OnEntityRemoved += onEntityRemoved;
		_poolGroup = pool.GetGroup(Matcher.PoolableGO);
	}
	
	void onEntityRemoved(Group group, Entity entity, int index, IComponent component) {
		Debug.Log("RemoveGameObjectSystem");
		GameObjectComponent gameObjectComponent = (GameObjectComponent)component;
		gameObjectComponent.gameObject.transform.parent = null;
		gameObjectComponent.gameObject.SetActive(false);
		addToPool(gameObjectComponent);
	}

	public void Execute(List<Entity> entities) {
		throw new Exception("Remove game object system called ^^");
	}

	void addToPool(GameObjectComponent gameObjectComponent) {
		string name = gameObjectComponent.path;
		bool wasObjectAdded = false;

		foreach (Entity e in _poolGroup.GetEntities()) {
			if (e.poolableGO.name.Equals(name)) {
				wasObjectAdded = true;
				e.poolableGO.queue.Enqueue(gameObjectComponent.gameObject);
			}
		}
		if (!wasObjectAdded) {
			Entity e = _pool.CreateEntity()
				.AddPoolableGO(name, new Queue<GameObject>());
			e.poolableGO.queue.Enqueue(gameObjectComponent.gameObject);
		}
	}
}
