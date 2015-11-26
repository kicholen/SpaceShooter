using Entitas;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerHealthBarSystem : IReactiveSystem {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Player, Matcher.Damage).OnEntityAdded(); } }
	
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
						bar.gameObject.gameObject.GetComponent<Slider>().value = component.currentValue / component.totalValue;
					}
				}
			}
		}
	}
}