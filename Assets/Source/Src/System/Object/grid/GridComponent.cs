using Entitas;

public enum GridState {
	BUSY = 1,
	FREE = 2
}

public class GridComponent : IComponent {
	public int type;
	public int sizeX;
	public int sizeY;
	public GridState[,] grid;
}