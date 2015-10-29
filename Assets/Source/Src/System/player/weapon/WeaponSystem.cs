using Entitas;
using System.Collections.Generic;

public class WeaponSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.Weapon.OnEntityAddedOrRemoved(); } }

	Group _group;

	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.PlayerModel);
	}

	public void Execute(List<Entity> entities) {
		PlayerModelComponent component = _group.GetSingleEntity().playerModel;
		foreach (Entity e in entities) {
			if (e.isWeapon) {
				e.AddMissileSpawner(0.0f, component.missileSpawnDelay, Resource.Missile, 0.0f, component.missileVelocity, CollisionTypes.Player);
			}
			else {
				e.RemoveMissileSpawner();
			}
		}
	}
}
