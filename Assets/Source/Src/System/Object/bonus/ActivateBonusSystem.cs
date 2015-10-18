using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBonusSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Bonus, Matcher.CollisionDeath).OnEntityAdded(); } }
	
	Pool _pool;
	Group _group;
	Group _player;

	public void SetPool(Pool pool) {
		Random.seed = 42;
		_pool = pool;
		_group = pool.GetGroup(Matcher.BonusModel);
		_player = pool.GetGroup(Matcher.Player);
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
		switch(bonus.type) {
		case 1:
			_pool.CreateEntity()
				.AddSpeedBonus(10.0f, 10.0f, 2.0f);
		break;
		default:
			throw new UnityException("Unknown bonus type: " + bonus.type);
		break;
		}
	}
}