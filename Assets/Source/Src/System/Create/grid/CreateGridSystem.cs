using Entitas;
using UnityEngine;

public class CreateGridSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {
		_pool.CreateEntity()
			.AddGrid(GridTypes.Full, 5, 5, getEmptyGrid(5, 5))
			.AddPosition(new Vector2(0.0f, 0.0f))
			.IsMoveWithCamera(true);
	}

	GridState[,] getEmptyGrid(int x, int y) {
		GridState[,] grid = new GridState[5,5];
		for (int i = 0; i < grid.GetLength(0); i++) {
			for (int j = 0; j < grid.GetLength(1); j++) {
				grid[i, j] = GridState.FREE;
			}
		}
		return grid;
	}
}