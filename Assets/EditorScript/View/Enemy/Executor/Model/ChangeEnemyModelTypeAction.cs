public class ChangeEnemyModelResourceAction : IEnemyModelCmpAction {
    string resource;

    public ChangeEnemyModelResourceAction(string resource) {
        this.resource = resource;
    }

    public void Execute(EnemyModel model) {
        model.resource = resource;
    }
}