using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawnerSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.BonusSpawner, Matcher.CollisionDeath).OnEntityAdded(); } }

	Pool _pool;
	Group _group;
	const float ACCELERATION = 1.0f;
	const float FRICTION = 0.45f;
	const float VELOCITY = 2.0f;

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
					if (Random.value <= model.probability) {
						spawnBonus(e, model);
					}
				}
			}
		}
	}

	void spawnBonus(Entity e, BonusModelComponent bonus) {
		PositionComponent position = e.position;
		int amount = Random.Range(bonus.minAmount, bonus.maxAmount);

		for (int i = 0; i < amount; i++) {
			float velocityX = Random.Range(-VELOCITY, VELOCITY);
			float velocityY = Random.Range(-VELOCITY, VELOCITY);
			float acceX = velocityX > 0.0f ? -ACCELERATION : ACCELERATION;
			float acceY = velocityY > 0.0f ? -ACCELERATION : ACCELERATION;
			_pool.CreateEntity()
				.AddBonus(bonus.type)
				.AddVelocity(velocityX, velocityY)
				.AddAcceleration(acceX, acceY, acceX > 0.0f ? -FRICTION : FRICTION, acceY > 0.0f ? -FRICTION : FRICTION, true)
				.AddPosition(position.x, position.y)
				.AddHealth(0)
				.AddCollision(CollisionTypes.Bonus)
				.AddResource(bonus.resource);
		}
	}
}