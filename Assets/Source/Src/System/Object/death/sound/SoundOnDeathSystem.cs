using Entitas;
using System.Collections.Generic;

public class SoundOnDeathSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.SoundOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }
	
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Execute(List<Entity> entities) {
		for (int i = 0; i < entities.Count; i++) {
			SoundOnDeathComponent component = entities[i].soundOnDeath;
			_pool.CreateEntity()
				.AddSound(component.resource, component.volume, null);
		}
	}
}