using Entitas;

public class CameraShakeComponent : IComponent {
	public float time;
	public float offsetX;
	public float offsetY;
	public float originalOffsetX;
	public float originalOffsetY;
}