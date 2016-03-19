public class ChangeEnemyFaceDirectionAction : IEnemyModelCmpAction
{
    bool faceDirection;

    public ChangeEnemyFaceDirectionAction(bool faceDirection)
    {
        this.faceDirection = faceDirection;
    }

    public void Execute(EnemyModel model)
    {
        model.faceDirection = faceDirection;
    }
}