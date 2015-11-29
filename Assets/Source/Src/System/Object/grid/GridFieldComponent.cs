using Entitas;

public enum GridFieldState {
	INACTIVE = 1,
	IDLE = 2,
	MOVE = 3
}

public class GridFieldComponent : IComponent {
	public GridFieldState state;
	public int type;
	public int x;
	public int y;
}