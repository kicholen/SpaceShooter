using Entitas;

public class DifficultyControllerSystem : IInitializeSystem, ISetPool {
	Pool pool;
	Group group;
	Group difficulties;
	
	public void SetPool(Pool pool) {
		this.pool = pool;
		this.pool.GetGroup(Matcher.SettingsModel).OnEntityUpdated += update;
		group = pool.GetGroup(Matcher.DifficultyController);
		difficulties = pool.GetGroup(Matcher.DifficultyModel);
    }

	public void Initialize() {
		pool.CreateEntity()
			.AddDifficultyController(DifficultyTypes.None, 0, 0, 0);
        updateDifficulty(pool.GetGroup(Matcher.SettingsModel).GetSingleEntity().settingsModel);
    }

	void update(Group group, Entity entity, int index, IComponent previousComponent, IComponent nextComponent) {
        SettingsModelComponent settings = (SettingsModelComponent)nextComponent;
        updateDifficulty(settings);
    }

    void updateDifficulty(SettingsModelComponent settings) {
        DifficultyControllerComponent difficulty = group.GetSingleEntity().difficultyController;

        if (settings.difficulty == difficulty.difficultyType) {
            return;
        }

        foreach (Entity e in difficulties.GetEntities()) {
            DifficultyModelComponent model = e.difficultyModel;
            if (settings.difficulty == model.type) {
                difficulty.dmgBoostPercent = model.dmgBoostPercent;
                difficulty.hpBoostPercent = model.hpBoostPercent;
                difficulty.missileSpeedBoostPercent = model.missileSpeedBoostPercent;
            }
        }
    }
}