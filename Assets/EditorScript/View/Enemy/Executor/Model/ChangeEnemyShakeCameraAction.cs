public class ChangeEnemyShakeCameraAction : IEnemyModelCmpAction
{
    int shakeCamera;

    public ChangeEnemyShakeCameraAction(int shakeCamera)
    {
        this.shakeCamera = shakeCamera;
    }

    public void Execute(EnemyModel model)
    {
        model.shakeCamera = shakeCamera;
    }
}