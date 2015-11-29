using Entitas;
using UnityEngine;

/*
 * Anchor - top-left corner
 */
public class GridSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _grid;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_grid = pool.GetGroup(Matcher.Grid);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.GridField, Matcher.Position));
		pool.GetGroup(Matcher.Path).OnEntityRemoved += onEntityPathRemoved;
	}
	
	public void Execute() {
		for (int i = 0; i < _group.count; i++) {
			Entity e = _group.GetEntities()[i];
			GridFieldComponent component = e.gridField;
			if (component.state == GridFieldState.IDLE) {
				PositionComponent position = e.position;
				Entity gridEntity = getGridEntity(component.type);
				GridComponent grid = gridEntity.grid;
				int previousX = component.x;
				int previousY = component.y;
				PositionComponent gridPosition = gridEntity.position;
				findField(grid.grid, GridState.FREE, out component.x, out component.y);
				grid.grid[component.x, component.y] = GridState.BUSY;
				if (previousX != -1) {
					grid.grid[previousX, previousY] = GridState.FREE;
				}

				component.state = GridFieldState.MOVE;
				Debug.Log(component.x + "x" + component.y);
				Vector2 to = new Vector2(gridPosition.pos.x + (float)component.x * grid.fieldSize, gridPosition.pos.y - (float)component.y * grid.fieldSize);
				e.AddTweenPosition(0.0f, 2.0f, EaseTypes.Linear, new Vector2(position.pos.x, position.pos.y), to, onComplete, null);
			}
			else if (component.state == GridFieldState.MOVE) {
				// do nothing
			}
		}
	}

	void onEntityPathRemoved(Group group, Entity entity, int index, IComponent component) {
		if (entity.hasGridField) {
			entity.gridField.state = GridFieldState.IDLE;
			entity.velocity.vel.Set(0.0f, 0.0f);
			entity.velocityLimit.maxVelocity = 0.0f;
			entity.isMoveWithCamera = true;
		}
	}

	void onComplete(Entity e) {
		e.gridField.state = GridFieldState.IDLE;
	}
	
	Entity getGridEntity(int type) {
		for (int i = 0; i < _grid.count; i++) {
			Entity e = _grid.GetEntities()[i];
			if (type == e.grid.type) {
				return e;
			}
		}
		return null;
	}

	void findField(GridState[,] grid, GridState state, out int x, out int y) {
		for (int i = 0; i < grid.GetLength(0); i++) {
			for (int j = 0; j < grid.GetLength(1); j++) {
				if (grid[i,j] == state) {
					x = i;
					y = j;
					return;
				}
			}
		}
		x = 0;
		y = 0;
	}
}