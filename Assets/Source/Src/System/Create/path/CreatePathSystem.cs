using Entitas;
using System.Collections.Generic;

public class CreatePathSystem : IInitializeSystem, ISetPool {
	Pool _pool;

	const int PATHS_COUNT = 10;

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