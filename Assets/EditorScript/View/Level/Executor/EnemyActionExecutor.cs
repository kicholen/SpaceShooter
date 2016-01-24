public class EnemyActionExecutor {
    EnemyModel model;

    public EnemyActionExecutor(EnemyModel model) {
        this.model = model;
    }

    public void Execute(IEnemyAction modifier) {
        modifier.Execute(model);
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