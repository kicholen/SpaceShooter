public class ChangeEnemyModelResourceAction : IEnemyModelCmpAction {
    string resource;

    public ChangeEnemyModelResourceAction(string resource) {
        this.resource = resource;
    }

    public void Execute(EnemyModelComponent model) {
        model.resource = resource;
    }
}