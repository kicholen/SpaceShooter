using Entitas;

public enum GridFieldState {
	INACTIVE = 1,
	IDLE = 2,
	TWEEN = 3
}

public class GridFieldComponent : IComponent {
	public float time;
	public float freezeDuration;
	public GridFieldState state;
	public int type;
	public int x;
	public int y;
}