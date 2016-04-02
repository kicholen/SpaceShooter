using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class BonusOnDeathSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.BonusOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }

	Pool pool;
	Group group;
	Group players;
    Group currentShip;

	const float MAX_TWEEN_RADIUS = 0.5f;

	const float VELOCITY = 5.0f;

    public void SetPool(Pool pool) {
		Random.seed = 42;
		this.pool = pool;
		group = pool.GetGroup(Matcher.BonusModel);
		players = pool.GetGroup(Matcher.Player);
		currentShip = pool.GetGroup(Matcher.CurrentShip);
    }
	
	public void Execute(List<Entity> entities) {
		Entity player = players.GetSingleEntity();
        float magentRadius = currentShip.GetSingleEntity().currentShip.model.magnetRadius;
        foreach (Entity e in entities) {
			BonusOnDeathComponent bonus = e.bonusOnDeath;
			int type = bonus.type;

			foreach (Entity bonusEntity in group.GetEntities()) {
				BonusModelComponent model = bonusEntity.bonusModel;
				if ((type & model.type) > 0) {
					if (Random.value <= model.probability) {
						spawnBonus(e, model, player, magentRadius);
					}
				}
			}
		}
	}

	void spawnBonus(Entity e, BonusModelComponent bonus, Entity follow, float magnetRadius) {
		Vector2 position = e.position.pos;
		int amount = Random.Range(bonus.minAmount, bonus.maxAmount);

		for (int i = 0; i < amount; i++) {
			float offsetX = Random.Range(-MAX_TWEEN_RADIUS, MAX_TWEEN_RADIUS);
			float offsetY = Random.Range(-MAX_TWEEN_RADIUS, MAX_TWEEN_RADIUS);
			Entity bonusEntity = pool.CreateEntity()
				.AddBonus(bonus.type)
				.AddVelocityLimit(VELOCITY)
				.AddVelocity(new Vector2())
                .AddTween(true, new List<Tween>())
				.AddPosition(new Vector2(position.x, position.y))
				.AddHealth(0)
				.AddCollision(CollisionTypes.Bonus, 0)
				.AddFollowTarget(follow)
				.AddMagnet(magnetRadius)
                .AddTweenOnDeath(0.5f, 1.4f)
				.AddResource(bonus.resource);
			bonusEntity.tween.AddTween(bonusEntity.position, EaseTypes.linear, PositionAccessorType.XY, 2.0f)
				.From(position.x, position.y)
				.To(position.x + offsetX, position.y + offsetY);
		}
	}
}