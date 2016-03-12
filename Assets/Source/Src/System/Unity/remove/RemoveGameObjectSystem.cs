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
		component.gameObject.SetActive(false);
        if (component.isPoolable)
            addToPool(component);
        else
            UnityEngine.Object.Destroy(component.gameObject);
	}

	void addToPool(GameObjectComponent gameObjectComponent) {
        string name = gameObjectComponent.path;
        GameObject go = gameObjectComponent.gameObject;
        resetGameObject(go);
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

    void resetGameObject(GameObject go) {
        go.transform.rotation = Quaternion.identity;
        go.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        resetColor(go);
    }

    void resetColor(GameObject go) {
        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        if (renderer != null) {
            Color color = renderer.color;
            renderer.color = new Color(color.r, color.g, color.b, 1);
        }
    }
}
