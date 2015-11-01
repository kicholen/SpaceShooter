using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBonusSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Bonus, Matcher.CollisionDeath).OnEntityAdded(); } }
	
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
			foreach (Entity bonusEntity in _group.GetEntities()) {
				BonusModelComponent bonusModel = bonusEntity.bonusModel;
				if ((bonus.type & bonusModel.type) > 0) {
					activateBonus(bonusEntity.bonusModel);
				}
			}
		}
	}
	
	void activateBonus(BonusModelComponent bonus) {
		switch(bonus.type) {
		case BonusTypes.Star:
			// do nothing
			break;
		case BonusTypes.Speed:
			_pool.CreateEntity()
				.AddSpeedBonus(15.0f, 0.0f, 2.0f);
		break;
		default:
			throw new UnityException("Unknown bonus type: " + bonus.type);
		}
	}
}