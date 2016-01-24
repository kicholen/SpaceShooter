using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class BonusOnDeathSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.BonusOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }

	Pool _pool;
	Group _group;
	Group _players;

	const float MAX_TWEEN_RADIUS = 1.0f;

	const float TEST_VELOCITY = 5.0f;
	const float TEST_RADIUS = 4.0f;

	public void SetPool(Pool pool) {
		Random.seed = 42;
		_pool = pool;
		_group = pool.GetGroup(Matcher.BonusModel);
		_players = pool.GetGroup(Matcher.Player);
	}
	
	public void Execute(List<Entity> entities) {
		Entity player = _players.GetSingleEntity();
		foreach (Entity e in entities) {
			BonusOnDeathComponent bonus = e.bonusOnDeath;
			int type = bonus.type;

			foreach (Entity bonusEntity in _group.GetEntities()) {
				BonusModelComponent model = bonusEntity.bonusModel;
				if ((type & model.type) > 0) {
					if (Random.value <= model.probability) {
						spawnBonus(e, model, player);
					}
				}
			}
		}
	}

	void spawnBonus(Entity e, BonusModelComponent bonus, Entity follow) {
		Vector2 position = e.position.pos;
		int amount = Random.Range(bonus.minAmount, bonus.maxAmount);

		for (int i = 0; i < amount; i++) {
			float offsetX = Random.Range(-MAX_TWEEN_RADIUS, MAX_TWEEN_RADIUS);
			float offsetY = Random.Range(-MAX_TWEEN_RADIUS, MAX_TWEEN_RADIUS);
			Entity bonusEntity = _pool.CreateEntity()
				.AddBonus(bonus.type)
				.AddVelocity(new Vector2())
				.AddTween(true, new List<Tween>())
				.AddPosition(new Vector2(position.x, position.y))
				.AddHealth(0)
				.AddCollision(CollisionTypes.Bonus, 0)
				.AddFollowTarget(follow)
				.AddMagnet(TEST_VELOCITY, TEST_RADIUS)
                .AddTweenOnDeath(0.5f, 1.4f)
				.AddResource(bonus.resource);
			bonusEntity.tween.AddTween(bonusEntity.position, EaseTypes.linear, PositionAccessorType.XY, 2.0f)
				.From(position.x, position.y)
				.To(position.x + offsetX, position.y + offsetY);
		}
	}
}