public class ChangeEnemyShakeCameraAction : IEnemyModelCmpAction
{
    int shakeCamera;

    public ChangeEnemyShakeCameraAction(int shakeCamera)
    {
        this.shakeCamera = shakeCamera;
    }

    public void Execute(EnemyModelComponent model)
    {
        model.shakeCamera = shakeCamera;
    }
}