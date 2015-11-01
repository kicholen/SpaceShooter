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
		float deltaTime = _time.GetSingleEntity().time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			navigateMissile(e, deltaTime);
		}
	}

	void navigateMissile(Entity e, float deltaTime) {
		//HomeMissileComponent homeMissile = e.homeMissile;
		FollowTargetComponent targetComponent = e.followTarget;
		if (targetComponent.target != null && targetComponent.target.hasGameObject) {
			GameObject target = targetComponent.target.gameObject.gameObject;

			Vector2 position = e.position.pos;
			Vector3 targetPosition = target.transform.position;
			VelocityComponent velocity = e.velocity;

			float velocityX = (targetPosition.x - position.x) * 15.0f;
			float velocityY = (targetPosition.y - position.y) * 15.0f;

			velocity.vel.Set(Mathf.Lerp(velocity.vel.x, velocityX, deltaTime), Mathf.Lerp(velocity.vel.y, velocityY, deltaTime));
		}
	}
}