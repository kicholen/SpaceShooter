using Entitas;

public class LaserSpawnerComponent : IComponent {
	public float velocity;
	public float height;
	public float angle;
	public int collisionType;
	public Entity laser;
}