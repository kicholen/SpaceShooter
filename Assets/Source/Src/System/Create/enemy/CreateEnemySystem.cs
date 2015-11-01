using Entitas;
using UnityEngine;

public class CreateEnemySystem : IInitializeSystem, ISetPool {
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddEnemy(0)
			.AddPosition(new Vector2(2.0f, 5.0f))
			.AddVelocity(new Vector2(0.0f, 0.0f))
			.AddVelocityLimit(5.0f)
			.AddHealth(20)
			.AddBonusOnDeath(BonusTypes.Speed)
			.AddCircleMissileSpawner(12, 1.0f, 5.0f, Resource.MissileEnemy, 2.0f, CollisionTypes.Enemy)
			.AddCollision(CollisionTypes.Enemy)
			.AddResource(Resource.Enemy);
	}
}