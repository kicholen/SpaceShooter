using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class PositionSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.Position.OnEntityAdded(); } }
	
	public void SetPool(Pool pool) {
		//pool.GetGroup(Matcher.Position).OnEntityAdded += OnEntityAdded;
	}
	/*
	void OnEntityAdded(Group group, Entity entity, int index, IComponent component) {
		GameObjectComponent gameObjectComponent = (GameObjectComponent)component;
		gameObjectComponent.gameObject.SetActive(false);
		gameObjectComponent.gameObject.transform.parent = null;
	}*/
	
	public void Execute(List<Entity> entities) {
		Debug.Log("PositionSystem");
		for (int i = 0; i < entities.Count; i++) {
			Entity e = entities [i];
			PositionComponent position = e.position;
			e.gameObject.gameObject.transform.position = new Vector3(position.x, position.y, 0);//.Set(position.x, position.y, 0.0f);
		}
	}
}
