using Entitas;

public class VelocitySystem : IExecuteSystem, ISetPool {
	Group _group;
	int asd;

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

		if (velocity.vel.magnitude > velocityLimit.maxVelocity) {
			velocity.vel.Normalize();
			velocity.vel *= velocityLimit.maxVelocity;
		}
	}
}
