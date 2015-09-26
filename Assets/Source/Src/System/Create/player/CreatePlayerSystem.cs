using Entitas;

public class CreatePlayerSystem : IInitializeSystem, ISetPool {
    Pool _pool;

	public void SetPool(Pool pool) {
        _pool = pool;
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddPlayer("superPlayer")
			.AddPosition(2, 2)
			.AddResource(Resource.Test);
	}
}
