using UnityEngine;
using Entitas;

public class CreateEnemySystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		Debug.Log("CreateEnemySystem");
		_pool.CreateEntity()
			.AddEnemy(0)
			.AddPosition(2.0f, 5.0f)
			.AddVelocity(0.0f, 0.0f)
			.AddVelocityLimit(5.0f, 5.0f)
			.AddHealth(2000)
			.AddCircleMissileSpawner(12, 5.0f, 5.0f, Resource.MissileEnemy, 0.0f, -2.0f, CollisionTypes.Enemy)
			.AddCollision(CollisionTypes.Enemy)
			.AddResource(Resource.Enemy);
	}
}