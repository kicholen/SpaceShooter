public class WaveActionExecutor {
    WaveModel model;

    public WaveActionExecutor(WaveModel model) {
        this.model = model;
    }

    public void Execute(IWaveAction modifier) {
        modifier.Execute(model);
    }

    public float GetSpawnBarrier() {
        return model.spawnBarrier;
    }

    public int GetCount() {
        return model.count;
    }

    public float GetSpawnOffset() {
        return model.spawnOffset;
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

    public int getGrid() {
        return model.grid;
    }

    public int getDamage() {
        return model.damage;
    }

    public int getType() {
        return model.type;
    }
}