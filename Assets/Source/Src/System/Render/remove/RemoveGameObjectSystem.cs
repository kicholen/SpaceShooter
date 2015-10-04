using UnityEngine;
using Entitas;
using System.Collections.Generic;
using System.Collections;

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
		GameObjectComponent gameObjectComponent = (GameObjectComponent)component;
		//Object.Destroy(gameObjectComponent.gameObject);
		gameObjectComponent.gameObject.transform.parent = null;
		gameObjectComponent.gameObject.SetActive(false);
		addToPool(gameObjectComponent);
	}

	public void Execute(List<Entity> entities) {
		Debug.Log("RemoveGameObjectSystem");
		for (int i = 0; i < entities.Count; i++) {// todo this is not called at all, change name to poolableSystem ^^
			Entity e = entities [i];
			e.RemoveGameObject();
			e.isDestroyEntity = true;
		}
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
