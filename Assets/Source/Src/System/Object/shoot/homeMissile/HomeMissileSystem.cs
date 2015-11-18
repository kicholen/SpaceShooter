using Entitas;
using UnityEngine;

public class HomeMissileSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.AllOf(Matcher.HomeMissile, Matcher.FollowTarget));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			navigateMissile(e, deltaTime);
		}
	}

	void navigateMissile(Entity e, float deltaTime) {
		HomeMissileComponent homeMissile = e.homeMissile;
		FollowTargetComponent targetComponent = e.followTarget;

		// Find out what type of thing we're targeting, in case of
		// it dies and we'll need another similiar target.
		if (homeMissile.targetCollisionType == CollisionTypes.Unknown) {
			if (targetComponent.target == null) {
				// this missile doesn't know it's target and can't find it. Please DIE!
				e.isDestroyEntity = true;
				return;
			}

			homeMissile.targetCollisionType = targetComponent.target.collision.collide;
		}

		if (targetComponent.target == null || !targetComponent.target.hasGameObject) {
			// The target is not alive, so go find another one.
			e.AddFindTarget(homeMissile.targetCollisionType);
			e.RemoveFollowTarget();
			return;
		}

		var targetEntity = targetComponent.target;

		Vector2 position = e.position.pos;
		Vector2 targetPosition = targetEntity.position.pos;
		VelocityComponent velocity = e.velocity;

		float velocityX = (targetPosition.x - position.x) * 15.0f;
		float velocityY = (targetPosition.y - position.y) * 15.0f;

		velocity.vel.Set(Mathf.Lerp(velocity.vel.x, velocityX, deltaTime), Mathf.Lerp(velocity.vel.y, velocityY, deltaTime));
	}
}
