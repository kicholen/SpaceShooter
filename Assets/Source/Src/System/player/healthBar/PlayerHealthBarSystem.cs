using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Player, Matcher.Damage).OnEntityAdded(); } }
	
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities) {
		for (int i = 0; i < entities.Count; i++) {
			Entity e = entities[i];

			if (e.hasParent) {
				List<Entity> children = e.parent.children;
				for (int j = 0; j < children.Count; j++) {
					Entity bar = children[j];
					if (bar.hasPlayerHealthBar) {
						PlayerHealthBarComponent component = bar.playerHealthBar;
						int damage = e.damage.damage;
						component.currentValue -= (float)damage;
						GameObject go = bar.gameObject.gameObject;
						Transform fgTransform = go.transform.FindChild("fg");
						float height = go.GetComponent<SpriteRenderer>().bounds.size.y;
						fgTransform.localScale = new Vector3(fgTransform.localScale.x, component.currentValue / component.totalValue, fgTransform.localScale.z);
						fgTransform.localPosition = new Vector3(0.0f, (fgTransform.localScale.y - 1.0f) * height / 2.0f / go.transform.localScale.y, fgTransform.localPosition.z);
					}
					else {
						Debug.Log("PlayerHealthBarSystem: should never be called");
					}
				}
			}
			else {
				Debug.Log("PlayerHealthBarSystem: should never be called");
			}
		}
	}
}