using Entitas;

public class MotherShipComponent : IComponent
{
    public float time;
    public float duration;
    public float timeRandomFactor;

    public int spawnedDronesCount;
    public int droneSpawnLimit;
    public int droneType;
    public int droneHealth;
    public int droneDamage;
    public float droneSpeed;
}