using Entitas;

public class VelocitySystem : IExecuteSystem, ISetPool {
	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.VelocityLimit, Matcher.Velocity));
	}
	
	public void Execute() {
		foreach (var e in _group.GetEntities()) {
			limitVelocity(e);
		}
	}

	void limitVelocity(Entity e) {
		VelocityLimitComponent velocityLimit = e.velocityLimit;
		VelocityComponent velocity = e.velocity;

		if (velocity.x > (velocityLimit.x + velocityLimit.offsetX)) {
			velocity.x = velocityLimit.x + velocityLimit.offsetX;
		}
		else if (velocity.x < -(velocityLimit.x + velocityLimit.offsetX)) {
			velocity.x = -(velocityLimit.x + velocityLimit.offsetX);
		}

		if (velocity.y > (velocityLimit.y + velocityLimit.offsetY)) {
			velocity.y = velocityLimit.y + velocityLimit.offsetY;
		}
		else if (velocity.y < -(velocityLimit.y + velocityLimit.offsetY)) {
			velocity.y = -(velocityLimit.y + velocityLimit.offsetY);
		}
	}
}
