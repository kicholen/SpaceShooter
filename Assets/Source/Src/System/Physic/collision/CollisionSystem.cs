using UnityEngine;
using Entitas;

public class CollisionSystem : IExecuteSystem, ISetPool {
	Group _group;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Collision, Matcher.GameObject));
	}
	
	public void Execute() {
		Debug.Log("CollisionSystem");
		foreach (var e in _group.GetEntities()) {
			checkCollision(e);
		}
	}
	
	void checkCollision(Entity e) {
		CollisionScript collision = e.gameObject.gameObject.GetComponent<CollisionScript>();

		if (collision.queue.Count > 0) {
			e.isDestroyEntity = true;
		}
	}
	
	float getAcceleration() {
		return 0.0f;
	}
}
