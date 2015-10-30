using Entitas;

public class DifficultyControllerSystem : IInitializeSystem, ISetPool {

	Pool _pool;
	Group _group;
	Group _models;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_pool.GetGroup(Matcher.SettingsModel).OnEntityUpdated += update;
		_group = pool.GetGroup(Matcher.DifficultyController);
		_models = pool.GetGroup(Matcher.DifficultyModel);
	}

	public void Initialize() {
		_pool.CreateEntity()
			.AddDifficultyController(DifficultyTypes.None, 0, 0, 0);
	}

	void update(Group group, Entity entity, int index, IComponent previousComponent, IComponent nextComponent) {
		SettingsModelComponent settings = (SettingsModelComponent)nextComponent;
		DifficultyControllerComponent difficulty = _group.GetSingleEntity().difficultyController;

		if (settings.difficulty == difficulty.difficultyType) {
			return;
		}

		foreach (Entity e in _models.GetEntities()) {
			DifficultyModelComponent model = e.difficultyModel;
			if (settings.difficulty == model.type) {
				difficulty.dmgBoostPercent = model.dmgBoostPercent;
				difficulty.hpBoostPercent = model.hpBoostPercent;
				difficulty.missileSpeedBoostPercent = model.missileSpeedBoostPercent;
			}
		}
	}
}