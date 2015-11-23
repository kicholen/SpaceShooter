using Entitas;

public class MovingBlockadeComponent : IComponent {
	public float offset;
	public float direction; // -1 / 1
	public float time;
	public float duration;
	public float stopDuration;
}