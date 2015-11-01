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
			FindTargetComponent component = e.findTarget;
			if (component.collisionType == CollisionTypes.Enemy) {
				if (_player.count > 0) {
					e.AddFollowTarget(_player.GetEntities()[0]);
				}
			}
			else if (component.collisionType == CollisionTypes.Player) {
				if (_enemies.count > 0) {
                    Vector2 pos = new Vector2(e.position.x, e.position.y);
                    float minDist = float.PositiveInfinity;
                    Entity chosenEnemy = null;
                    Vector2 enemyPos = new Vector2();

                    foreach (Entity enemy in _enemies.GetEntities()) {
                        enemyPos.Set(enemy.position.x, enemy.position.y);
                        float dist = Vector2.Distance(pos, enemyPos);
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