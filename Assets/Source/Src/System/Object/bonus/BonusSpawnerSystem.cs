using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawnerSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.BonusSpawner, Matcher.CollisionDeath).OnEntityAdded(); } }

	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		Random.seed = 42;
		_pool = pool;
		_group = pool.GetGroup(Matcher.BonusModel);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			BonusSpawnerComponent bonus = e.bonusSpawner;
			int type = bonus.type;

			foreach (Entity bonusEntity in _group.GetEntities()) {
				BonusModelComponent model = bonusEntity.bonusModel;
				if ((model.type & type) == 1) {
					if (Random.value < model.probability) {
						spawnBonus(e, model);
					}
				}
			}
		}
	}

	void spawnBonus(Entity e, BonusModelComponent bonus) {
		PositionComponent position = e.position;
		_pool.CreateEntity()
			.AddBonus(bonus.type)
			.AddPosition(position.x, position.y)
			.AddHealth(0)
			.AddCollision(CollisionTypes.Bonus)
			.AddResource(Resource.Bonus);
	}
}