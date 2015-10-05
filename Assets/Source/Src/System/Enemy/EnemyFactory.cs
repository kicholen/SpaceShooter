using Entitas;

public class EnemyFactory {
	Pool _pool;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void createEnemyByType(int type, float x, float y, float velocityX, float velocityY, int health) {
		Entity e;
		switch(type) {
		case 0:
			e = createStandardEnemy(type, x, y, velocityX, velocityY, health);
			e.isFaceDirection = true;
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, CollisionTypes.Enemy, 0.0f, -4.0f, false);
		break;

		default:
			e = createStandardEnemy(type, x, y, velocityX, velocityY, health);
		break;
		}
	}

	Entity createStandardEnemy(int type, float x, float y, float velocityX, float velocityY, int health) {
		return _pool.CreateEntity()
			.AddEnemy(type)
			.AddPosition(x, y)
			.AddVelocity(velocityX, velocityY)
			.AddVelocityLimit(5.0f, 5.0f)
			.AddHealth(health)
			.AddCollision(CollisionTypes.Enemy)
			.AddResource(Resource.Enemy);
	}
}
