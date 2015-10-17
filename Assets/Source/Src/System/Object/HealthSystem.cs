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
				_pool.CreateEntity()
					.AddCameraShake(0.5f, 0.1f, 0.0f);
				spawnParticles(e.position);
			}

			e.RemoveDamage();
		}
	}

	void spawnParticles(PositionComponent position) {
		_pool.CreateEntity()
			.AddPosition(position.x, position.y)
			.AddParticleSpawn(10, Resource.Particle, 0.5f, 2.0f);

	}
}