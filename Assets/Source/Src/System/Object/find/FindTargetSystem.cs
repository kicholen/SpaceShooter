using Entitas;
using System.Collections.Generic;

public class FindTargetSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.FindTarget.OnEntityAdded(); } }
	
	Pool _pool;
	Group _player;
	Group _enemies;

	public void SetPool(Pool pool) {
		_pool = pool;
		_player = pool.GetGroup(Matcher.Player);
		_enemies = pool.GetGroup(Matcher.Enemy);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			FindTargetComponent component = e.findTarget;
			if (component.collisionType == CollisionTypes.Enemy) {
				if (_player.Count > 0) {
					e.AddFollowTarget(_player.GetEntities()[0]);
				}
			}
			else if (component.collisionType == CollisionTypes.Player) {
				if (_enemies.Count > 0) {
					e.AddFollowTarget(_enemies.GetEntities()[0]);
				}
			}
			e.RemoveFindTarget();
		}
	}
}