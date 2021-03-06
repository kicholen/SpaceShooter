﻿public class EnemyActionExecutor {
    EnemySpawnModel model;

    public EnemyActionExecutor(EnemySpawnModel model) {
        this.model = model;
    }

    public void Execute(IEnemyAction modifier) {
        modifier.Execute(model);
    }

    public float GetSpawnBarrier() {
        return model.spawnBarrier;
    }

    public float GetPosX() {
        return model.posX;
    }

    public float GetPosY() {
        return model.posY;
    }

    public float getSpeed() {
        return model.speed;
    }

    public int getHealth() {
        return model.health;
    }

    public int getPath() {
        return model.path;
    }

    public int getDamage() {
        return model.damage;
    }

    public int getType() {
        return model.type;
    }
}