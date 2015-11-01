using Entitas;
using UnityEngine;

public class ParticleSpawnSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.AllOf(Matcher.ParticleSpawn, Matcher.Position));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			spawnParticles(e.particleSpawn, e.position.pos);
			e.isDestroyEntity = true;
		}
	}

	void spawnParticles(ParticleSpawnComponent particle, Vector2 position) {
		float angleOffset = getAngle(particle.amount);
		float angle = 0.0f;
		float baseVelX = particle.velocity;
		float baseVelY = particle.velocity;
		
		for (int i = 0; i < particle.amount; i++) {
			float cosinus = Mathf.Cos(angle);
			float sinus = Mathf.Sin(angle);
			float velocityX = baseVelX * cosinus - baseVelY * sinus;
			float velocityY = baseVelX * sinus + baseVelY * cosinus;
			_pool.CreateEntity()
				.AddPosition(new Vector2().Set(position))
				.AddVelocity(velocityX, velocityY)
				.AddHealth(0)
				.AddDestroyEntityDelayed(particle.lifespan)
				.AddResource(particle.resource)
				.AddAlpha(particle.lifespan, particle.lifespan);
			angle = angle + angleOffset;
		}
	}
	
	float getAngle(int amount) {
		return Mathf.Deg2Rad * (360.0f / (float)amount);
	}
}