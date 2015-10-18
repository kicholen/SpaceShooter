using Entitas;

public class HealthSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Damage, Matcher.Health));
	}	
	
	public void Execute() {
		foreach(Entity e in _group.GetEntities()) {
			HealthComponent health = e.health;
			DamageComponent damage = e.damage;
			health.health -= damage.damage;

			if (health.health < 0) {
				e.isDestroyEntity = true;
				e.isCollisionDeath = true;
			}

			e.RemoveDamage();
		}
	}
}