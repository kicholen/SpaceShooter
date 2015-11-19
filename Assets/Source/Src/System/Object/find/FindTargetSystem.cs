using Entitas;
using System.Collections.Generic;
using UnityEngine;

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
			if (!e.hasFindTarget) {
				return;
			}
			FindTargetComponent component = e.findTarget;
			if (component.targetCollisionType == CollisionTypes.Player) {
				if (_player.count > 0) {
					e.AddFollowTarget(_player.GetSingleEntity());
				}
			}
			else if (component.targetCollisionType == CollisionTypes.Enemy) {
 				if (_enemies.count > 0) {
					float minDist = float.PositiveInfinity;
					Entity chosenEnemy = null;

					foreach (Entity enemy in _enemies.GetEntities()) {
						float dist = Vector2.Distance(e.position.pos, enemy.position.pos);
						if (dist < minDist) {
							chosenEnemy = enemy;
							minDist = dist;
						}
					}

					e.AddFollowTarget(chosenEnemy);
                }
			}
			e.RemoveFindTarget();
		}
	}
}