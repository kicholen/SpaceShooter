using UnityEngine;
using Entitas;
using System.Collections.Generic;
using System;

public class CollisionSystem : IExecuteSystem, ISetPool {
	Group _group;
	Dictionary<string, int> savedDamage = new Dictionary<string, int>();

	const string interlude = "_";

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Collision, Matcher.Health));
	}
	
	public void Execute() {
		Debug.Log("CollisionSystem");
		foreach(var e in _group.GetEntities()) {
			checkCollision(e);
		}
	}
	
	void checkCollision(Entity e) {
		CollisionScript collision = e.gameObject.gameObject.GetComponent<CollisionScript>();
		Queue<string> queue = collision.queue;
		if (queue.Count == 0) {
			return;
		}

		HealthComponent health = e.health;
		int damage = getDamageDone(collision.queue, health.health);

		if (damage > 0) {
			e.AddDamage(damage);
		}
		else {
			throw new Exception("Sth is fucked up, and is unnecessary calculating damage");
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
