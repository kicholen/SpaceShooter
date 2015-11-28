using Entitas;

public class CreateDifficultySystem : IInitializeSystem, ISetPool {
	Pool _pool;

	const string EASY = "Easy";
	const string NORMAL = "Normal";
	const string HARD = "Hard";

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddComponent(ComponentIds.DifficultyModel, Utils.Deserialize<DifficultyModelComponent>(EASY));
		_pool.CreateEntity()
			.AddComponent(ComponentIds.DifficultyModel, Utils.Deserialize<DifficultyModelComponent>(NORMAL));
		_pool.CreateEntity()
			.AddComponent(ComponentIds.DifficultyModel, Utils.Deserialize<DifficultyModelComponent>(HARD));
	}
}