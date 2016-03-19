using Entitas;
using System.Collections.Generic;

public class CreatePathSystem : IInitializeSystem, ISetPool {
	Pool pool;

	const int PATHS_COUNT = 51;

	public void SetPool(Pool pool) {
		this.pool = pool;
	}
	
	public void Initialize() {
        Entity e = pool.CreateEntity()
            .AddPathsModel(new Dictionary<string, PathModel>());
        for (int i = 0; i < PATHS_COUNT; i++)
        {
            PathModel model = Utils.Deserialize<PathModel>(i.ToString());
            e.pathsModel.map.Add(model.name, model);
        }
	}
}