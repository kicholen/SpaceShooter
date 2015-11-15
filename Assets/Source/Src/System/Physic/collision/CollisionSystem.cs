using Entitas;
using System.Collections.Generic;
using System;

public class CollisionSystem : IExecuteSystem, ISetPool {

	Group _group;
	Group _difficulty;
	Dictionary<string, int> savedDamage = new Dictionary<string, int>();

	const string interlude = "_";

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
		Queue<string> queue = collision.queue;
		if (queue.Count == 0) {
			return;
		}

		HealthComponent health = e.health;
		int damage = getDamageDone(collision.queue, health.health);
		if (!e.hasPlayer) {
			damage = damage * (difficulty.dmgBoostPercent + 100) / 100;
		}
		if (damage > 0) {
			e.AddDamage(damage);
		}
	}

	int getDamageDone(Queue<string> queue, int health) {
		bool flag = true;
		int result = 0;

		while (flag) {
			string colliderName = queue.Dequeue();
			if (savedDamage.ContainsKey(colliderName)) {
				result += savedDamage[colliderName];
			}
			else {
				int damage = Convert.ToInt16(colliderName.Substring(0, colliderName.IndexOf(interlude)));
				savedDamage[colliderName] = damage;
				result += damage;
			}

			if (queue.Count == 0 || result > health) {
				flag = false;
			}
		}

		return result;
	}
}
