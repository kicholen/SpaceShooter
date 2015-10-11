using Entitas;

public class CreatePlayerSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddPlayer("superPlayer")
			.AddPosition(0.0f, 0.0f)
			.AddVelocity(0.0f, 0.0f)
			//.AddAcceleration(0.0f, 0.0f, 0.0f, 0.0f)
			.AddVelocityLimit(5.0f, 5.0f)
			.AddCollision(CollisionTypes.Player)
			.AddHealth(50)
			.AddResource(Resource.Player);
	}
}