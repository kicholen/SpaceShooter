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
}