using Entitas;
using System.Collections.Generic;

public class CreatePlayerSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	Group _group;

	public void SetPool(Pool pool) {
		_pool = pool;
		pool.GetGroup(Matcher.PlayerModel).OnEntityAdded += create;
		_group = pool.GetGroup(Matcher.PlayerModel);
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddComponent(ComponentIds.PlayerModel, Utils.DeserializeComponent(typeof(PlayerModelComponent)));
	}

	void create(Group group, Entity entity, int index, IComponent component) {
		PlayerModelComponent playerModel = (PlayerModelComponent)component;
		
		Entity player = _pool.CreateEntity()
				.AddPlayer(playerModel.name)
				.AddPosition(0.0f, 0.0f)
				.AddVelocity(0.0f, 0.0f)
				.AddVelocityLimit(playerModel.velocityLimit.x, playerModel.velocityLimit.y, 0.0f, 0.0f)
				.AddCollision(CollisionTypes.Player)
				.AddHealth(playerModel.health)
				.AddResource(Resource.Player);
		
		player.AddParent(getChildren(player, playerModel));
	}

	List<Entity> getChildren(Entity parent, PlayerModelComponent component) {
		List<Entity> children = new List<Entity>();
		if (component.hasHomeMissile) {
	         children.Add(_pool.CreateEntity()
		         .AddRelativePosition(0.5f, 0.5f)
		         .AddPosition(0.0f, 0.0f)
		         .AddChild(parent)
	             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, Resource.Missile, component.homeMissileVelocity, CollisionTypes.Player)
		         .AddResource(Resource.Weapon));
	         children.Add(_pool.CreateEntity()
		         .AddRelativePosition(-0.5f, 0.5f)
		         .AddPosition(0.0f, 0.0f)
		         .AddChild(parent)
	             .AddHomeMissileSpawner(0.0f, component.homeMissileSpawnDelay, Resource.Missile, component.homeMissileVelocity, CollisionTypes.Player)
		         .AddResource(Resource.Weapon));
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