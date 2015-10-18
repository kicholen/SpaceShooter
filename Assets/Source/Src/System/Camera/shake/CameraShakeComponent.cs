using Entitas;

public class CameraShakeComponent : IComponent {
	public float time;
	public float totalTime;

	public float totalOffsetX;
	public float offsetX = 0.0f;
	public int direction = -1;
	public float velocity = 0.0f;
	public bool velocityCalculated = false;
}