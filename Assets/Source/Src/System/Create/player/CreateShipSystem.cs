﻿using Entitas;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateShipSystem : IInitializeSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateShip.OnEntityAdded(); } }

	Pool _pool;
	Group _currentShip;

	const int SHIPS_COUNT = 2;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_currentShip = _pool.GetGroup(Matcher.CurrentShip);
		for (int i = 1; i <= SHIPS_COUNT; i++) {
			_pool.CreateEntity()
				.AddComponent(ComponentIds.ShipModel, Utils.Deserialize<ShipModelComponent>(i.ToString()));
		}
	}

	public void Execute(List<Entity> entities) {
		entities[0].isDestroyEntity = true;
		createShip(_currentShip.GetSingleEntity().currentShip.model);
	}

	void createShip(ShipModelComponent shipModel) {
		Entity ship = _pool.CreateEntity()
			.AddPlayer(shipModel.name)
			.AddPosition(new Vector2(0.0f, 0.0f))
			.AddVelocity(new Vector2(0.0f, 0.0f))
			.AddVelocityLimit(shipModel.maxVelocity)
			.AddCollision(CollisionTypes.Player, 10)
			.AddHealth(shipModel.health)
			.AddResource(ResourceWithColliders.Player)
			.AddExplosionOnDeath(1.0f, Resource.Explosion)
			.AddGhost(0.0f, 0.1f, 0.3f) 
			.IsMoveWithCamera(true);
		
		ship.AddParent(getShipChildren(ship, shipModel));

		createUI(ship);
	}

	void createUI(Entity ship) {
		createHealthBar(ship);
		createIndicatorPanel(ship);
        createScoreText(ship);
        createKillInfoText(ship);
    }

	void createHealthBar(Entity player) {
		Entity e = _pool.CreateEntity()
			.AddPlayerHealthBar((float)player.health.health, (float)player.health.health)
			.AddUIResource(UIResource.UIHealthBar)
			.AddChild(player);
		player.parent.children.Add(e);
	}

	void createIndicatorPanel(Entity player) {
		Entity e = _pool.CreateEntity()
			.IsIndicatorPanel(true)
			.AddUIResource(UIResource.UIIndicator)
			.AddChild(player);
		player.parent.children.Add(e);
    }

    void createScoreText(Entity player)
    {
        Entity e = _pool.CreateEntity()
            .IsPlayerScore(true)
            .AddUIResource(UIResource.UIScore)
            .AddChild(player);
        player.parent.children.Add(e);
    }

    void createKillInfoText(Entity player)
    {
        Entity e = _pool.CreateEntity()
            .IsKillInfo(true)
            .AddUIResource(UIResource.UIKillInfo)
            .AddChild(player);
        player.parent.children.Add(e);
    }

    List<Entity> getShipChildren(Entity parent, ShipModelComponent component) {
		List<Entity> children = new List<Entity>();
		if (component.hasHomeMissile) {
			//addHomeMissile(children, parent, component);
		}
		if (component.hasSecondaryMissiles) {
			addSecondaryMissiles(children, parent);
		}
        //addHelperShips(children, parent, component);
        //addShield(children, parent, component);

		addNonRemovable(children);
		return children;
	}

    void addShield(List<Entity> children, Entity parent, ShipModelComponent component) {
        children.Add(_pool.CreateEntity()
            .AddPosition(new Vector2(0.0f, 0.0f))
            .AddRelativePosition(0.0f, 0.0f)
            .AddChild(parent)
            .AddCollision(CollisionTypes.Player, 10)
            .AddHealth(component.health * 100)
            .AddShieldCollision(0.0f, 0.1f, new Queue<Vector2>())
            .IsCollisionPosition(true)
            .AddResource(ResourceWithColliders.PlayerShield));
    }

    void addHomeMissile(List<Entity> children, Entity parent, ShipModelComponent component) {
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(0.5f, -0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(parent)
		             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, component.homeMissileDamage, ResourceWithColliders.MissileHoming, component.homeMissileVelocity, new Vector2(component.homeMissileVelocity * 0.6f, -1.2f), 0.25f, 3.0f, CollisionTypes.Player));
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(-0.5f, -0.5f)
		             .AddPosition(new Vector2(0.0f, 0.0f))
		             .AddChild(parent)
		             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, component.homeMissileDamage, ResourceWithColliders.MissileHoming, component.homeMissileVelocity, new Vector2(-component.homeMissileVelocity * 0.6f, -1.2f), 0.25f, 3.0f, CollisionTypes.Player));
	}

	void addSecondaryMissiles(List<Entity> children, Entity parent) {
		Entity leftWeapon = _pool.CreateEntity()
			.AddRelativePosition(-0.25f, -0.25f)
			.AddPosition(new Vector2(0.0f, 0.0f))
			.AddChild(parent)
			.IsSecondaryWeapon(true);
		
		Entity rightWeapon = _pool.CreateEntity()
			.AddRelativePosition(0.25f, -0.25f)
			.AddPosition(new Vector2(0.0f, 0.0f))
			.AddChild(parent)
			.IsSecondaryWeapon(true);
		
		children.Add(leftWeapon);
		children.Add(rightWeapon);
	}

	void addHelperShips(List<Entity> children, Entity parent, ShipModelComponent component) {
		children.Add(_pool.CreateEntity()
			.AddPosition(new Vector2(0.5f, 0.0f))
			.AddResource(Resource.Player)
			.AddChild(parent)
		    .IsLeaderFollower(true)
			.AddVelocity(new Vector2())
			.AddVelocityLimit(5.0f)
            .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, component.homeMissileDamage, ResourceWithColliders.MissileHoming, component.homeMissileVelocity, new Vector2(-component.homeMissileVelocity, -1.0f), 1.0f, 3.0f, CollisionTypes.Player));
		children.Add(_pool.CreateEntity()
			.AddPosition(new Vector2(0.0f, 0.5f))
			.AddResource(Resource.Player)
			.AddChild(parent)
            .IsLeaderFollower(true)
			.AddVelocity(new Vector2())
			.AddVelocityLimit(5.5f)
            .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, component.homeMissileDamage, ResourceWithColliders.MissileHoming, component.homeMissileVelocity, new Vector2(-component.homeMissileVelocity, -1.0f), 1.0f, 3.0f, CollisionTypes.Player));
		children.Add(_pool.CreateEntity()
			.AddPosition(new Vector2(-0.5f, 0.0f))
			.AddResource(Resource.Player)
			.AddChild(parent)
            .IsLeaderFollower(true)
		    .AddVelocity(new Vector2())
		    .AddVelocityLimit(6.0f)
            .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, component.homeMissileDamage, ResourceWithColliders.MissileHoming, component.homeMissileVelocity, new Vector2(-component.homeMissileVelocity, -1.0f), 1.0f, 3.0f, CollisionTypes.Player));
	}
	
	void addNonRemovable(List<Entity> entities) {
		foreach (Entity e in entities) {
			e.isNonRemovable = true;
		}
	}
}