using Entitas;

public class CreateSettingsSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddComponent(ComponentIds.SettingsModel, Utils.Deserialize<SettingsModelComponent>());
	}
}