using Entitas;
using System.Collections.Generic;

public class FindTargetSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.FindTarget.OnEntityAdded(); } }
	
	Group _player;
	Group _enemies;

	public void SetPool(Pool pool) {
		_player = pool.GetGroup(Matcher.Player);
		_enemies = pool.GetGroup(Matcher.Enemy);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			FindTargetComponent component = e.findTarget;
			if (component.collisionType == CollisionTypes.Enemy) {
				if (_player.count > 0) {
					e.AddFollowTarget(_player.GetEntities()[0]);
				}
			}
			else if (component.collisionType == CollisionTypes.Player) {
				if (_enemies.count > 0) {
					e.AddFollowTarget(_enemies.GetEntities()[0]);
				}
			}
			e.RemoveFindTarget();
		}
	}
}