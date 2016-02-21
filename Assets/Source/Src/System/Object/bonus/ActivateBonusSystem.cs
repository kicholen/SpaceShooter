using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBonusSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Bonus, Matcher.CollisionDeath).OnEntityAdded(); } }

    const int shieldPower = 200;
    Pool pool;
	Group group;
	Group currentShip;
    Group player;
    Group shipBonus;

    public void SetPool(Pool pool) {
		this.pool = pool;
		group = pool.GetGroup(Matcher.BonusModel);
		currentShip = pool.GetGroup(Matcher.CurrentShip);
        player = pool.GetGroup(Matcher.Player);
        shipBonus = pool.GetGroup(Matcher.ShipBonus);
    }

    public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			BonusComponent bonus = e.bonus;
			foreach (Entity bonusEntity in group.GetEntities()) {
				BonusModelComponent bonusModel = bonusEntity.bonusModel;
				if ((bonus.type & bonusModel.type) > 0) {
					activateBonus(bonusEntity.bonusModel);
				}
			}
		}
	}
	
	void activateBonus(BonusModelComponent bonus) {
		switch(bonus.type) {
		    case BonusTypes.Star:
			    // do nothing
			break;
            case BonusTypes.Speed:
                activateSpeedBonus();
                break;
            case BonusTypes.Laser:
                activateLaser();
                break;
            case BonusTypes.Shield:
                activateShield();
                break;
            case BonusTypes.Atom:
                spawnAtom();
                break;
            case BonusTypes.FireRate:
                activateFireRateBoost();
                break;
            case BonusTypes.Damage:
                activateDamageBoost();
                break;
            default:
			throw new UnityException("Unknown bonus type: " + bonus.type);
		}
    }

    void activateDamageBoost()
    {
        Entity entity = shipBonus.GetSingleEntity();
        entity.shipBonus.damageBoost = 0.5f;
        player.GetSingleEntity().isWeapon = false;
        resetBoosts(entity);
    }

    void activateFireRateBoost()
    {
        Entity entity = shipBonus.GetSingleEntity();
        entity.shipBonus.fireRateBoost = 0.5f;
        player.GetSingleEntity().isWeapon = false;
        resetBoosts(entity);
    }

    void resetBoosts(Entity entity)
    {
        if (entity.hasDelayedCall)
            entity.RemoveDelayedCall();
        entity.AddDelayedCall(5.0f, ent => {
            if (ent != null)
            {
                ent.shipBonus.damageBoost = 0;
                ent.shipBonus.fireRateBoost = 0;
            }
            player.GetSingleEntity().isWeapon = false;
        });
    }

    void spawnAtom()
    {
        pool.CreateEntity()
            .IsAtomBomb(true);
    }

    void activateShield()
    {
        Entity playerEntity = player.GetSingleEntity();

        if (!hasShield(playerEntity.parent.children))
            playerEntity.parent.children.Add(createShieldEntity(playerEntity));
    }

    bool hasShield(List<Entity> children)
    {
        for (int i = 0; i < children.Count; i++)
            if (children[i].hasShieldCollision)
                return true;

        return false;
    }

    Entity createShieldEntity(Entity parent)
    {
        return pool.CreateEntity()
            .AddPosition(new Vector2(0.0f, 0.0f))
            .AddRelativePosition(0.0f, 0.0f)
            .AddChild(parent)
            .AddCollision(CollisionTypes.Player, 10)
            .AddHealth(shieldPower)
            .AddShieldCollision(0.0f, 0.1f, new Queue<Vector2>())
            .IsCollisionPosition(true)
            .IsNonRemovable(true)
            .AddResource(ResourceWithColliders.PlayerShield);
    }

    void activateLaser()
    {
        Entity playerEntity = player.GetSingleEntity();

        if (!playerEntity.hasLaserSpawner)
        {
            playerEntity.AddLaserSpawner(5.0f, 10.0f, 10.0f, 0.0f, new Vector2(), CollisionTypes.Player, 1, Resource.Laser, null)
                .AddDelayedCall(5.0f, deactivateLaser);
        }
    }

    void activateSpeedBonus()
    {
        pool.CreateEntity()
            .AddSpeedBonus(15.0f, currentShip.GetSingleEntity().currentShip.model.maxVelocity, 2.0f);
    }

    void deactivateLaser(Entity player)
    {
        if (player != null && player.hasLaserSpawner)
        {
            player.RemoveLaserSpawner();
        }
    }
}