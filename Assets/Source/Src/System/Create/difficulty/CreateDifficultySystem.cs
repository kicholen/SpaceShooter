using Entitas;

public class CreateDifficultySystem : IInitializeSystem, ISetPool {
    const int DIFFICULTIES_COUNT = 3;

    Pool pool;

	public void SetPool(Pool pool) {
		this.pool = pool;
	}
	
	public void Initialize() {
        for (int i = 1; i <= DIFFICULTIES_COUNT; i++) {
            pool.CreateEntity()
                .AddComponent(ComponentIds.DifficultyModel, Utils.Deserialize<DifficultyModelComponent>(i.ToString()));
        }
	}
}