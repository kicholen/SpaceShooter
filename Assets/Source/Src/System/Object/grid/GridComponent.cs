using Entitas;
using UnityEngine;

public enum GridState {
	BUSY = 1,
	FREE = 2
}

public class GridComponent : IComponent {
	public int type;
	public float fieldSize;
	public GridState[,] grid;
}