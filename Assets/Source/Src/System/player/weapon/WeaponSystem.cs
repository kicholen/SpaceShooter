using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.Weapon.OnEntityAddedOrRemoved(); } }

	Group group;
    Group bonus;

    public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.CurrentShip);
		bonus = pool.GetGroup(Matcher.ShipBonus);
    }

	public void Execute(List<Entity> entities) {
		ShipModelComponent component = group.GetSingleEntity().currentShip.model;
        ShipBonusComponent bonusComponent = bonus.count > 0 ? bonus.GetSingleEntity().shipBonus : null;

        foreach (Entity e in entities) {
			if (e.isWeapon)
                addWeapons(component, bonusComponent, e);
            else
                removeWeapons(component, e);
        }
	}

    void addWeapons(ShipModelComponent component, ShipBonusComponent bonusComponent, Entity e)
    {
        float missileSpawnDelay = bonusComponent == null ? component.missileSpawnDelay : component.missileSpawnDelay * (1.0f - bonusComponent.fireRateBoost);
        float damageFactor = bonusComponent == null ? 1.0f : 1.0f + bonusComponent.damageBoost;
        e.AddMissileSpawner(0.0f, (int)((float)component.missileDamage * damageFactor), missileSpawnDelay, ResourceWithColliders.MissilePrimary, new Vector2(0.0f, component.missileVelocity), CollisionTypes.Player);
        if (component.hasSecondaryMissiles)
        {
            List<Entity> children = e.parent.children;
            for (int i = 0; i < children.Count; i++)
            {
                Entity child = children[i];
                if (child.isSecondaryWeapon)
                {
                    child.AddMissileSpawner(0.0f, (int)((float)component.secondaryMissileDamage * damageFactor), missileSpawnDelay, ResourceWithColliders.MissileSecondary, new Vector2(0.0f, component.missileVelocity), CollisionTypes.Player);
                }
            }
        }
    }

    void removeWeapons(ShipModelComponent component, Entity e)
    {
        if (e.hasMissileSpawner)
        {
            e.RemoveMissileSpawner();

            if (component.hasSecondaryMissiles)
            {
                List<Entity> children = e.parent.children;
                for (int i = 0; i < children.Count; i++)
                {
                    Entity child = children[i];
                    if (child.isSecondaryWeapon)
                    {
                        child.RemoveMissileSpawner();
                    }
                }
            }
        }
    }
}
