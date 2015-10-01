using Entitas;

public class SnapPositionComponent : IComponent {
	public float x;
	public float y;
	public float width;
	public float height;

	public bool shouldSnapToCameraY;
}