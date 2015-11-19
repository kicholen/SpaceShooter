using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class RemoveGameObjectSystem : ISystem, ISetPool, IEnsureComponents {
	public IMatcher ensureComponents { get { return Matcher.GameObject; } }

	Group _poolGroup;
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
		pool.GetGroup(Matcher.GameObject).OnEntityRemoved += onEntityRemoved;
		_poolGroup = pool.GetGroup(Matcher.PoolableGO);
	}
	
	void onEntityRemoved(Group group, Entity entity, int index, IComponent component) {
		removeGameObject((GameObjectComponent)component);
		if (entity.hasParent) {
			ParentComponent parentComponent = entity.parent;
			for (int i = 0; i < parentComponent.children.Count; i++) {
				Entity child = parentComponent.children[i];
				if (child != null && child.hasGameObject) {
					removeGameObject(child.gameObject);
				}
			}
		}
	}

	void removeGameObject(GameObjectComponent component) {
		component.gameObject.transform.parent = null;
		component.gameObject.SetActive(false);
		addToPool(component);
	}

	void addToPool(GameObjectComponent gameObjectComponent) {
		string name = gameObjectComponent.path;
		GameObject go = gameObjectComponent.gameObject;
		go.transform.rotation = Quaternion.identity;
		bool wasObjectAdded = false;

		foreach (Entity e in _poolGroup.GetEntities()) {
			if (e.poolableGO.name.Equals(name)) {
				wasObjectAdded = true;
				e.poolableGO.queue.Enqueue(go);
				break;
			}
		}
		if (!wasObjectAdded) {
			Entity e = _pool.CreateEntity()
				.AddPoolableGO(name, new Queue<GameObject>());
			e.poolableGO.queue.Enqueue(go);
		}
	}
}
