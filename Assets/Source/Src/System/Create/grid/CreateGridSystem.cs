using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class CreateGridSystem : IInitializeSystem, IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.CreateGrid.OnEntityAdded(); } }
	Pool _pool;
	
	public void SetPool(Pool pool) {
		_pool = pool;
	}
	
	public void Initialize() {

	}

	public void Execute(List<Entity> entities) {
		entities[0].isDestroyEntity = true;

		_pool.CreateEntity()
			.AddGrid(GridTypes.Full, 1.0f, getEmptyGrid(7, 5))
			.AddPosition(new Vector2(-2.0f, 9.0f))
			.AddVelocity(new Vector2(0.0f, 0.0f))
			.AddVelocityLimit(0.0f)
			.IsMoveWithCamera(true);
	}

	GridState[,] getEmptyGrid(int x, int y) {
		GridState[,] grid = new GridState[x, y];
		for (int i = 0; i < grid.GetLength(0); i++) {
			for (int j = 0; j < grid.GetLength(1); j++) {
				grid[i, j] = GridState.FREE;
			}
		}
		return grid;
	}
}