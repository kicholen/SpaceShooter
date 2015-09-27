using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class RemoveGameObjectSystem : IReactiveSystem, ISetPool, IEnsureComponents {
	public TriggerOnEvent trigger { get { return Matcher.Resource.OnEntityRemoved(); } }
	
	public IMatcher ensureComponents { get { return Matcher.GameObject; } }
	
	public void SetPool(Pool pool) {
		pool.GetGroup(Matcher.GameObject).OnEntityRemoved += onEntityRemoved;
	}
	
	void onEntityRemoved(Group group, Entity entity, int index, IComponent component) {
		GameObjectComponent gameObjectComponent = (GameObjectComponent)component;
		Object.Destroy(gameObjectComponent.gameObject);
	}

	public void Execute(List<Entity> entities) {
		Debug.Log("RemoveGameObjectSystem");
		for (int i = 0; i < entities.Count; i++) {
			Entity e = entities [i];
			e.RemoveGameObject();
			e.isDestroyEntity = true;
		}
	}
}
