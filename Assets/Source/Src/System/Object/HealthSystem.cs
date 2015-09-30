using Entitas;
using UnityEngine;

public class HealthSystem : IExecuteSystem, ISetPool {
	Group _group;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Damage, Matcher.Health));
	}
	
	public void Execute() {
		Debug.Log("HealthSystem");
		foreach(Entity e in _group.GetEntities()) {
			HealthComponent health = e.health;
			DamageComponent damage = e.damage;
			health.health -= damage.damage;

			if (health.health < 0) {
				e.isDestroyEntity = true;
			}
			else {
				e.RemoveDamage();
			}
		}
	}
}