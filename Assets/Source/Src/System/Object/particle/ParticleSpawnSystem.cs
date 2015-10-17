using Entitas;
using UnityEngine;

public class ParticleSpawnSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.AllOf(Matcher.ParticleSpawn, Matcher.Position));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _group.GetEntities()) {
			spawnParticles(e.particleSpawn, e.position);
			e.isDestroyEntity = true;
		}
	}


	void spawnParticles(ParticleSpawnComponent particle, PositionComponent position) {
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
				.AddPosition(position.x, position.y)
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