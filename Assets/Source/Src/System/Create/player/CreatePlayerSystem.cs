using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerSystem : IInitializeSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreatePlayer.OnEntityAdded(); } }

	Pool _pool;
	Group _playerModel;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_playerModel = _pool.GetGroup(Matcher.PlayerModel);
		_pool.CreateEntity()
			.AddComponent(ComponentIds.PlayerModel, Utils.DeserializeComponent(typeof(PlayerModelComponent)));
	}

	public void Execute(List<Entity> entities) {
		entities[0].isDestroyEntity = true;
		createPlayer(_playerModel.GetSingleEntity().playerModel);
	}

	void createPlayer(PlayerModelComponent playerModel) {
		Entity player = _pool.CreateEntity()
			.AddPlayer(playerModel.name)
			.AddPosition(new Vector2(0.0f, 0.0f))
			.AddVelocity(new Vector2(0.0f, 0.0f))
			.AddVelocityLimit(playerModel.maxVelocity)
			.AddCollision(CollisionTypes.Player)
			.AddHealth(playerModel.health)
			.AddResource(Resource.Player)
			.AddExplosionOnDeath(1.0f, Resource.Explosion)
			.IsMoveWithCamera(true);
		
		player.AddParent(getPlayerChildren(player, playerModel));
	}
	
	List<Entity> getPlayerChildren(Entity parent, PlayerModelComponent component) {
		List<Entity> children = new List<Entity>();
		if (component.hasHomeMissile) {
			children.Add(_pool.CreateEntity()
			             .AddRelativePosition(0.5f, -0.5f)
			             .AddPosition(new Vector2(0.0f, 0.0f))
			             .AddChild(parent)
			             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, Resource.MissilePrimary, component.homeMissileVelocity, CollisionTypes.Player)
			             .AddResource(Resource.Weapon));
			children.Add(_pool.CreateEntity()
			             .AddRelativePosition(-0.5f, -0.5f)
			             .AddPosition(new Vector2(0.0f, 0.0f))
			             .AddChild(parent)
			             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, Resource.MissilePrimary, component.homeMissileVelocity, CollisionTypes.Player)
			             .AddResource(Resource.Weapon));
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