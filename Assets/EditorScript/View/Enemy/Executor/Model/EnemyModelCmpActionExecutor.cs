public class EnemyModelCmpActionExecutor {
    EnemyModelComponent model;

    public EnemyModelCmpActionExecutor(EnemyModelComponent model) {
        this.model = model;
    }

    public void Execute(IEnemyModelCmpAction modifier) {
        modifier.Execute(model);
    }

    public int getType() {
        return model.type;
    }

    public string getResource() {
        return model.resource;
    }

    public int getWeapon() {
        return model.weapon;
    }

    public bool getFaceDirection()
    {
        return model.faceDirection;
    }

    public float getRandomRotation()
    {
        return model.randomRotation;
    }

    public int getShakeCamera()
    {
        return model.shakeCamera;
    }
}