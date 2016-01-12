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

    int count = DEFAULT_COUNT;
    float spawnOffset = DEFAULT_SPAWN_OFFSET;
    float speed = DEFAULT_SPEED;
    int type = DEFAULT_TYPE;
    int health = DEFAULT_HEALTH;
    int path = DEFAULT_PATH;
    int grid = DEFAULT_GRID;
    int damage = DEFAULT_DAMAGE;

    WaveModel model;

    public AddWaveAction(float spawnBarrier) {
        this.spawnBarrier = spawnBarrier;
    }

    public void Execute(LevelModelComponent component) {
        model = createWaveModel();
        component.waves.Add(model);
    }

    public WaveModel getModel() {
        if (model == null) {
            throw new Exception("First call Execute");
        }
        return model;
    }

    WaveModel createWaveModel() {
        model = new WaveModel();
        model.spawnBarrier = spawnBarrier;
        model.count = count;
        model.spawnOffset = spawnOffset;
        model.speed = speed;
        model.type = type;
        model.health = health;
        model.path = path;
        model.grid = grid;
        model.damage = damage;
        return model;
    }
}
