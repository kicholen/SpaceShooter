using Entitas;

public class CollisionSystem : IExecuteSystem, ISetPool
{
	Group group;
	Group difficulty;

	public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.AllOf(Matcher.Collision, Matcher.Health, Matcher.GameObject));
		difficulty = pool.GetGroup(Matcher.DifficultyController);
	}
	
	public void Execute() {
		DifficultyControllerComponent difficultyComponent = difficulty.GetSingleEntity().difficultyController;
		foreach(Entity e in group.GetEntities()) {
			checkCollision(e, difficultyComponent);
		}
	}
	
	void checkCollision(Entity e, DifficultyControllerComponent difficulty) {
		CollisionScript collision = e.gameObject.gameObject.GetComponent<CollisionScript>();
		int damageTaken = collision.DamageTaken;
		if (damageTaken == 0) {
			return;
		}

		e.AddDamage(damageTaken);
		collision.DamageTaken = 0;
	}
}
