using Entitas;
using UnityEngine;

public class HomeMissileSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _time;
	Group _missiles;

    Vector2 tempVector = new Vector2();
    const float Kp = 0.08f;

    public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.AllOf(Matcher.HomeMissile, Matcher.FollowTarget));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
            HomeMissileComponent homeMissile = e.homeMissile;
            homeMissile.delay -= deltaTime;
            if (homeMissile.delay < 0.0f) {
                navigateMissile(e, homeMissile);
            }
		}
	}

	void navigateMissile(Entity e, HomeMissileComponent homeMissile) {
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

        setMissileVelocity(e, homeMissile, targetComponent);
    }

    void setMissileVelocity(Entity e, HomeMissileComponent homeMissile, FollowTargetComponent targetComponent) {
        Entity targetEntity = targetComponent.target;

        Vector2 position = e.position.pos;
        Vector2 targetPosition = targetEntity.position.pos;
        VelocityComponent velocity = e.velocity;

        tempVector.Set((targetPosition.x - position.x), (targetPosition.y - position.y));
        tempVector.Normalize();
        tempVector *= homeMissile.speed;
        tempVector -= velocity.vel;
        tempVector *= Kp;
        velocity.vel += tempVector;
    }
}
