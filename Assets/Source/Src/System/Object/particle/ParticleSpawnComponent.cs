using Entitas;

public class ParticleSpawnComponent : IComponent {
	public int amount;
	public string resource;
	public float velocity;
	public float lifespan;
}