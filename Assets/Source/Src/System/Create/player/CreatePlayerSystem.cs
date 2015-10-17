using Entitas;
using System.Collections.Generic;

public class CreatePlayerSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		Entity e = _pool.CreateEntity()
			.AddPlayer("superPlayer")
			.AddPosition(0.0f, 0.0f)
			.AddVelocity(0.0f, 0.0f)
			.AddVelocityLimit(5.0f, 5.0f, 0.0f, 0.0f)
			.AddCollision(CollisionTypes.Player)
			.AddHealth(50)
			.AddLaserSpawner(5.0f, 0.0f, null)
			.AddResource(Resource.Player);
		e.AddParent(getChildren(e));
	}

	List<Entity> getChildren(Entity parent) {
		List<Entity> children = new List<Entity>();
		children.Add(_pool.CreateEntity()
		             	.AddRelativePosition(0.5f, 0.5f)
		             	.AddPosition(0.0f, 0.0f)
		             	.AddChild(parent)
		             	.AddHomeMissileSpawner(0.0f, 0.5f, Resource.Missile, 5.0f, CollisionTypes.Player)
		             	.AddResource(Resource.Weapon));
		children.Add(_pool.CreateEntity()
		             .AddRelativePosition(-0.5f, 0.5f)
		             .AddPosition(0.0f, 0.0f)
		             .AddChild(parent)
		             .AddHomeMissileSpawner(0.0f, 0.5f, Resource.Missile, 5.0f, CollisionTypes.Player)
		             .AddResource(Resource.Weapon));
		return children;
	}
}