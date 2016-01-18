using System;

public class AddEnemyAction : ILevelAction {
    const float DEFAULT_SPEED = 5.0f;
    const int DEFAULT_TYPE = 1;
    const int DEFAULT_HEALTH = 100;
    const int DEFAULT_PATH = 1;
    const int DEFAULT_DAMAGE = 10;
    const float DEFAULT_POSX = 0.0f;

    float spawnBarrier;
    float posY;

    EnemyModel model;

    public AddEnemyAction(float spawnBarrier) {
        this.spawnBarrier = spawnBarrier;
        this.posY = spawnBarrier;
    }

    public void Execute(LevelModelComponent component) {
        model = createEnemyModel();
        component.enemies.Add(model);
    }

    public EnemyModel getModel() {
        if (model == null) {
            throw new Exception("First call Execute");
        }
        return model;
    }

    EnemyModel createEnemyModel() {
        model = new EnemyModel();
        model.spawnBarrier = spawnBarrier;
        model.posY = posY;
        model.posX = DEFAULT_POSX;
        model.speed = DEFAULT_SPEED;
        model.type = DEFAULT_TYPE;
        model.health = DEFAULT_HEALTH;
        model.path = DEFAULT_PATH;
        model.damage = DEFAULT_DAMAGE;
        return model;
    }
}
