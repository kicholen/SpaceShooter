using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.FindTarget.OnEntityAdded(); } }
	
	Group player;
	Group enemies;

	public void SetPool(Pool pool) {
		player = pool.GetGroup(Matcher.Player);
		enemies = pool.GetGroup(Matcher.Enemy);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			if (!e.hasFindTarget) {
				return;
			}
			FindTargetComponent component = e.findTarget;
			if (component.targetCollisionType == CollisionTypes.Player) {
				if (player.count > 0) {
					e.AddFollowTarget(player.GetSingleEntity());
				}
			}
			else if (component.targetCollisionType == CollisionTypes.Enemy) {
 				if (enemies.count > 0)
                {
                    e.AddFollowTarget(findNearestEnemy(e));
                }
            }
            e.RemoveFindTarget();
		}
	}

    Entity findNearestEnemy(Entity e)
    {
        float minDist = float.PositiveInfinity;
        Entity chosenEnemy = null;

        foreach (Entity enemy in enemies.GetEntities())
        {
            float dist = Vector2.Distance(e.position.pos, enemy.position.pos);
            if (dist < minDist)
            {
                chosenEnemy = enemy;
                minDist = dist;
            }
        }

        return chosenEnemy;
    }
}