using System;

public class AddWaveAction : ILevelAction {
    const int DEFAULT_COUNT = 3;
    const float DEFAULT_SPAWN_OFFSET = 0.2f;
    const float DEFAULT_SPEED = 5.0f;
    const int DEFAULT_TYPE = 1;
    const int DEFAULT_HEALTH = 100;
    const int DEFAULT_PATH = 1;
    const int DEFAULT_GRID = 0;
    const int DEFAULT_DAMAGE = 10;

    float spawnBarrier;

    WaveSpawnModel model;

    public AddWaveAction(float spawnBarrier) {
        this.spawnBarrier = spawnBarrier;
    }

    public void Execute(LevelModelComponent component) {
        model = createWaveModel();
        component.waves.Add(model);
    }

    public WaveSpawnModel getModel() {
        if (model == null) {
            throw new Exception("First call Execute");
        }
        return model;
    }

    WaveSpawnModel createWaveModel() {
        model = new WaveSpawnModel();
        model.spawnBarrier = spawnBarrier;
        model.count = DEFAULT_COUNT;
        model.spawnOffset = DEFAULT_SPAWN_OFFSET;
        model.speed = DEFAULT_SPEED;
        model.type = DEFAULT_TYPE;
        model.health = DEFAULT_HEALTH;
        model.path = DEFAULT_PATH;
        model.grid = DEFAULT_GRID;
        model.damage = DEFAULT_DAMAGE;
        return model;
    }
}
