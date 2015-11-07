using Entitas;
using UnityEngine;

public class CreateEnemySystem : IInitializeSystem, ISetPool {
	Pool _pool;
	Group _group;

	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.PathModel);
	}
	
	public void Initialize() {
		for (int i = 0; i < 5; i++) {
			_pool.CreateEntity()
				.AddEnemy(0)
				.AddPosition(new Vector2(4.0f, 4.0f - (float)i))
				.AddVelocity(new Vector2(0.0f, 0.0f))
				.AddVelocityLimit(5.0f)
				.AddHealth(5000)
				.AddPath(0, 4.0f - (float)i, _group.GetEntities()[0].pathModel)
				.AddBonusOnDeath(BonusTypes.Speed)
				.AddCircleMissileSpawner(12, 1.0f, 5.0f, Resource.MissileEnemy, 2.0f, CollisionTypes.Enemy)
				.AddCollision(CollisionTypes.Enemy)
				.AddResource(Resource.Enemy)
				.isFaceDirection = true;

			_pool.CreateEntity()
				.AddEnemy(0)
				.AddPosition(new Vector2(-4.0f, 4.0f - (float)i))
				.AddVelocity(new Vector2(0.0f, 0.0f))
				.AddVelocityLimit(5.0f)
				.AddHealth(5000)
				.AddPath(0, 4.0f - (float)i, _group.GetEntities()[1].pathModel)
				.AddBonusOnDeath(BonusTypes.Speed)
				.AddCircleMissileSpawner(12, 1.0f, 5.0f, Resource.MissileEnemy, 2.0f, CollisionTypes.Enemy)
				.AddCollision(CollisionTypes.Enemy)
				.AddResource(Resource.Enemy)
				.isFaceDirection = true;
		}
	}
}