using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDeathSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.ParticlesOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }
	
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			ParticlesOnDeathComponent component = e.particlesOnDeath;
			if (component.type == 1) {
				spawnParticles(e.position);
			}
			else {
				throw new UnityException("ParticlesOnDeathSystem: unknown type");
			}
		}
	}

	void spawnParticles(PositionComponent position) {
		_pool.CreateEntity()
			.AddPosition(position.x, position.y)
			.AddParticleSpawn(10, Resource.Particle, 0.5f, 2.0f);
		
	}
}