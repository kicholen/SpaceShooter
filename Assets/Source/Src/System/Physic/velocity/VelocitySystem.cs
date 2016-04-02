using Entitas;

public class VelocitySystem : IExecuteSystem, ISetPool {
	Group group;

	public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.AllOf(Matcher.VelocityLimit, Matcher.Velocity));
	}
	
	public void Execute() {
		foreach (var e in group.GetEntities()) {
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
