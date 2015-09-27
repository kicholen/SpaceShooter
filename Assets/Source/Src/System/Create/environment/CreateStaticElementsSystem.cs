using Entitas;

public class CreateStaticElementsSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddPosition(0.0f, 0.0f)
			.AddResource(Resource.Blockade);
	}
}