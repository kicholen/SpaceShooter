public class ChangeEnemyFaceDirectionAction : IEnemyModelCmpAction
{
    bool faceDirection;

    public ChangeEnemyFaceDirectionAction(bool faceDirection)
    {
        this.faceDirection = faceDirection;
    }

    public void Execute(EnemyModelComponent model)
    {
        model.faceDirection = faceDirection;
    }
}