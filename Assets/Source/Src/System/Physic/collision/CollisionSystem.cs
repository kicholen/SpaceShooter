using Entitas;

public class CollisionSystem : IExecuteSystem, ISetPool {

	Group _group;
	Group _difficulty;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Collision, Matcher.Health));
		_difficulty = pool.GetGroup(Matcher.DifficultyController);
	}
	
	public void Execute() {
		DifficultyControllerComponent difficulty = _difficulty.GetSingleEntity().difficultyController;
		foreach(Entity e in _group.GetEntities()) {
			checkCollision(e, difficulty);
		}
	}
	
	void checkCollision(Entity e, DifficultyControllerComponent difficulty) {
		CollisionScript collision = e.gameObject.gameObject.GetComponent<CollisionScript>();
		int damageTaken = collision.DamageTaken;
		if (damageTaken == 0) {
			return;
		}

		if (!e.hasPlayer) {
			damageTaken = damageTaken * (difficulty.dmgBoostPercent + 100) / 100;
		}
		if (damageTaken > 0) {
			e.AddDamage(damageTaken);
		}
	}
}
