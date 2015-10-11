using Entitas;

public class EnemyFactory {
	Pool _pool;
	Group _players;

	public void SetPool(Pool pool) {
		_pool = pool;
		_players = pool.GetGroup(Matcher.Player);
	}
	
	public void createEnemyByType(int type, float x, float y, float velocityX, float velocityY, int health) {
		Entity e;
		switch(type) {
		case 0:
			e = createStandardEnemy(type, x, y, velocityX, velocityY, health);
			e.isFaceDirection = true;
			e.AddMissileSpawner(0.0f, 2.5f, Resource.MissileEnemy, 0.0f, -4.0f, CollisionTypes.Enemy);
		break;
		case 1:
			e = createStandardEnemy(type, x, y, velocityX, velocityY, health);
			e.isFaceDirection = true;
			Entity player = _players.GetSingleEntity();
			e.AddHomeMissileSpawner(player.gameObject.gameObject, 0.0f, 2.0f, Resource.MissileEnemy, 5.0f, CollisionTypes.Enemy);
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
			.AddVelocityLimit(5.0f, 5.0f, 0.0f, 0.0f)
			.AddHealth(health)
			.AddCollision(CollisionTypes.Enemy)
			.AddBonusSpawner(1)
			.AddResource(Resource.Enemy);
	}
}
