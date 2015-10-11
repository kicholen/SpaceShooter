using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class WeaponSystem : IReactiveSystem {
	public TriggerOnEvent trigger { get { return Matcher.Weapon.OnEntityAddedOrRemoved(); } }
	
	public void Execute(List<Entity> entities) {
		Debug.Log("WeaponSystem");
		foreach (Entity e in entities) {
			if (e.isWeapon) {
				e.AddMissileSpawner(0.0f, 0.5f, Resource.Missile, 0.0f, 10.0f, CollisionTypes.Player);
			}
			else {
				e.RemoveMissileSpawner();
			}
		}
	}
}
