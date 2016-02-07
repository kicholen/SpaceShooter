using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.Weapon.OnEntityAddedOrRemoved(); } }

	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.CurrentShip);
	}

	public void Execute(List<Entity> entities) {
		ShipModelComponent component = _group.GetSingleEntity().currentShip.model;
		foreach (Entity e in entities) {
			if (e.isWeapon) {
				e.AddMissileSpawner(0.0f, component.missileDamage, component.missileSpawnDelay, ResourceWithColliders.MissilePrimary, new Vector2(0.0f, component.missileVelocity), CollisionTypes.Player);
				if (component.hasSecondaryMissiles) {
					List<Entity> children = e.parent.children;
					for (int i = 0; i < children.Count; i++) {
						Entity child = children[i];
						if (child.isSecondaryWeapon) {
                            child.AddMissileSpawner(0.0f, component.secondaryMissileDamage, component.missileSpawnDelay, ResourceWithColliders.MissileSecondary, new Vector2(0.0f, component.missileVelocity), CollisionTypes.Player);
                        }
                    }
				}
			}
			else {
				if (e.hasMissileSpawner) {
					e.RemoveMissileSpawner();

					if (component.hasSecondaryMissiles) {
						List<Entity> children = e.parent.children;
						for (int i = 0; i < children.Count; i++) {
							Entity child = children[i];
							if (child.isSecondaryWeapon) {
								child.RemoveMissileSpawner();
							}
						}
					}
				}
			}
		}
	}
}
