using Entitas;
using System.Collections.Generic;

public class DeadPlayerSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.DestroyEntity, Matcher.Player).OnEntityAdded(); } }
	
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}

	public void Execute(List<Entity> entities) {
	}
}
