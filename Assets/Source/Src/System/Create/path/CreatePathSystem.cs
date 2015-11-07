using Entitas;

public class CreatePathSystem : IInitializeSystem, ISetPool {
	Pool _pool;

	const int PATHS_COUNT = 12;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		for (int i = 0; i < PATHS_COUNT; i++) {
			_pool.CreateEntity()
				.AddComponent(ComponentIds.PathModel, Utils.DeserializeComponent(typeof(PathModelComponent), i.ToString()));
		}
	}
}