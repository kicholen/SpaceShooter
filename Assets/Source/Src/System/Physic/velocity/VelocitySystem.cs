using Entitas;

public class VelocitySystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Acceleration, Matcher.Velocity));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (var e in _group.GetEntities()) {
			applyVelocity(e, deltaTime);
			limitVelocity(e);
		}
	}

	void applyVelocity(Entity e, float deltaTime) {
		AccelerationComponent acceleration = e.acceleration;
		VelocityComponent velocity = e.velocity;

		e.ReplaceVelocity(velocity.x + deltaTime * acceleration.x, velocity.y + deltaTime * acceleration.y);
	}

	void limitVelocity(Entity e) {
		if (!e.hasVelocityLimit) {
			return;
		}
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
