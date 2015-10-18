using Entitas;

public class CreateEnemySystem : IInitializeSystem, ISetPool {
	Pool _pool;
	Group _players;

	public void SetPool(Pool pool) {
		_pool = pool;
		_players = pool.GetGroup(Matcher.Player);
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddEnemy(0)
			.AddPosition(2.0f, 5.0f)
			.AddVelocity(0.0f, 0.0f)
			.AddVelocityLimit(5.0f, 5.0f, 0.0f, 0.0f)
			.AddHealth(20)
			.AddBonusOnDeath(1)
			.AddCircleMissileSpawner(12, 1.0f, 5.0f, Resource.MissileEnemy, 0.0f, -2.0f, CollisionTypes.Enemy)
			.AddCollision(CollisionTypes.Enemy)
			.AddResource(Resource.Enemy);
	}
}