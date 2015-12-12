using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class CreateShipSystem : IInitializeSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateShip.OnEntityAdded(); } }

	Pool _pool;
	Group _currentShip;

	const int PLAYERS_COUNT = 2;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_currentShip = _pool.GetGroup(Matcher.CurrentShip);
		for (int i = 1; i <= PLAYERS_COUNT; i++) {
			_pool.CreateEntity()
				.AddComponent(ComponentIds.ShipModel, Utils.Deserialize<ShipModelComponent>(i.ToString()));
		}
		_pool.CreateEntity()
			.AddCurrentShip(_pool.GetGroup(Matcher.ShipModel).GetEntities()[0].shipModel);
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
		// create UI
		createHealthBar(ship);
		createIndicatorPanel(ship);
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
	
	List<Entity> getShipChildren(Entity parent, ShipModelComponent component) {
		List<Entity> children = new List<Entity>();
		if (component.hasHomeMissile) {
			children.Add(_pool.CreateEntity()
			             .AddRelativePosition(0.5f, -0.5f)
			             .AddPosition(new Vector2(0.0f, 0.0f))
			             .AddChild(parent)
			             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, component.homeMissileDamage, ResourceWithColliders.MissilePrimary, component.homeMissileVelocity, CollisionTypes.Player));
			children.Add(_pool.CreateEntity()
			             .AddRelativePosition(-0.5f, -0.5f)
			             .AddPosition(new Vector2(0.0f, 0.0f))
			             .AddChild(parent)
			             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, component.homeMissileDamage, ResourceWithColliders.MissilePrimary, component.homeMissileVelocity, CollisionTypes.Player));
		}
		if (component.hasSecondaryMissiles) {
			Entity leftWeapon = _pool.CreateEntity()
				.AddRelativePosition(-0.25f, -0.25f)
				.AddPosition(new Vector2(0.0f, 0.0f))
				.AddChild(parent);
			leftWeapon.isSecondaryWeapon = true;
			
			Entity rightWeapon = _pool.CreateEntity()
				.AddRelativePosition(0.25f, -0.25f)
				.AddPosition(new Vector2(0.0f, 0.0f))
				.AddChild(parent);
			rightWeapon.isSecondaryWeapon = true;
			
			children.Add(leftWeapon);
			children.Add(rightWeapon);
		}
		
		addNonRemovable(children);
		return children;
	}
	
	void addNonRemovable(List<Entity> entities) {
		foreach (Entity e in entities) {
			e.isNonRemovable = true;
		}
	}
}