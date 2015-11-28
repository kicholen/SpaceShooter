using Entitas;

public class CreateBonusSystem : IInitializeSystem, ISetPool {
	Pool _pool;

	const int BONUSES_COUNT = 2;

	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		for (int i = 1; i <= BONUSES_COUNT; i++) {
			_pool.CreateEntity()
				.AddComponent(ComponentIds.BonusModel, Utils.Deserialize<BonusModelComponent>(i.ToString()));
		}
	}
}