using Entitas;

public class HomeMissileSpawnerComponent : IComponent {
	public float time;
	public float spawnDelay;
	public string resource;
	public float velocity;

	///<summary>
	/// Collision type of owner.
	/// </summary>
	public int collisionType;
}