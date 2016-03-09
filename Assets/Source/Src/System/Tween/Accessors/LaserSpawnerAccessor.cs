using Entitas;

public static class LaserSpawnerAccessorType
{
    public const int ANGLE = 1;
}

public class LaserSpawnerAccessor : ITweenAccessor
{

    public int GetValues(IComponent target, int tweenType, float[] returnValues)
    {
        LaserSpawnerComponent targetLaserSpawner = (LaserSpawnerComponent)target;
        switch (tweenType)
        {
            case LaserSpawnerAccessorType.ANGLE:
                returnValues[0] = targetLaserSpawner.angle;
                return 1;
            default:
                return 0;
        }
    }

    public void SetValues(IComponent target, int tweenType, float[] newValues)
    {
        LaserSpawnerComponent targetLaserSpawner = (LaserSpawnerComponent)target;
        switch (tweenType)
        {
            case LaserSpawnerAccessorType.ANGLE:
                targetLaserSpawner.angle = newValues[0];
                break;
            default:
                break;
        }
    }
}