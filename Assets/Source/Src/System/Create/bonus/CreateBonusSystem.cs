using Entitas;

public class CreateBonusSystem : IInitializeSystem, ISetPool {
	Pool pool;

	const int BONUSES_COUNT = 7;

	public void SetPool(Pool pool) {
		this.pool = pool;
	}
	
	public void Initialize() {
		for (int i = 1; i <= BONUSES_COUNT; i++) {
			pool.CreateEntity()
				.AddComponent(ComponentIds.BonusModel, Utils.Deserialize<BonusModelComponent>(i.ToString()));
		}
	}
}