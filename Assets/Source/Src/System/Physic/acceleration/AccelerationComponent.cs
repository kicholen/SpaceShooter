using Entitas;

public class AccelerationComponent : IComponent {
	public float x;
	public float y;

	public float frictionX;
	public float frictionY;

	public bool stopNearZero;
}