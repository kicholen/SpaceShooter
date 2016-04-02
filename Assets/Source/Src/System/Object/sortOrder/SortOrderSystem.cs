using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class SortOrderSystem : IReactiveSystem {
	public TriggerOnEvent trigger { get { return Matcher.SortOrder.OnEntityAdded(); } }

	Group camera;
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			Vector3 position = e.gameObject.gameObject.transform.position;
			e.gameObject.gameObject.transform.position = new Vector3(position.x, position.y, e.sortOrder.z);
			e.RemoveSortOrder();
		}
	}
}