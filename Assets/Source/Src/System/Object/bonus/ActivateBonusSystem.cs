using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBonusSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Bonus, Matcher.DestroyEntity).OnEntityAdded(); } }
	
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		Random.seed = 42;
		_pool = pool;
		_group = pool.GetGroup(Matcher.BonusModel);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			BonusComponent bonus = e.bonus;
			int type = bonus.type;
			
			foreach (Entity bonusEntity in _group.GetEntities()) {
				BonusModelComponent model = bonusEntity.bonusModel;
				if ((model.type & type) == 1) {
					if (Random.value < model.probability) {
						activateBonus(e, model);
					}
				}
			}
		}
	}
	
	void activateBonus(Entity e, BonusModelComponent bonus) {
		Debug.Log("bonus activated");
	}
}