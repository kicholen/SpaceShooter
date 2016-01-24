using Entitas;

public class TargetMissileSpawnerComponent : IComponent {
    public float time;
    public int damage;
    public float spawnDelay;
    public string resource;
    public float velocity;
    public int collisionType;
    public int targetCollisionType;
}