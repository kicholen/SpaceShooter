using Entitas;
using UnityEngine;

public class SpawnMissileSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _missiles;
	float _deltaTime;

	public void SetPool(Pool pool) {
		_pool = pool;
		_missiles = _pool.GetGroup(Matcher.MissileSpawner);
	}
	
	public void Execute() {
		Debug.Log("SpawnMissileSystem");
		_deltaTime = Time.deltaTime;
		foreach (Entity e in _missiles.GetEntities()) {
			MissileSpawnerComponent missile = e.missileSpawner;
			missile.time -= _deltaTime;
			if (missile.time < 0.0f) {
				missile.time = missile.spawnDelay;
				spawnMissile(e.position);
			}
		}
	}
	
	void spawnMissile(PositionComponent position) {
		_pool.CreateEntity()
			.AddPosition(position.x, position.y)
			.AddVelocity(0.0f, 10.0f)
			.AddCollision(CollisionTypes.Player)
			.AddResource(Resource.Missile);
	}

}