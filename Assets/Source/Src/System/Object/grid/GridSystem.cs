using Entitas;

/*
 * Anchor - top-left corner
 */
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : IExecuteSystem, ISetPool {
	Group _grid;
	Group _time;
	Group _group;
	
	public void SetPool(Pool pool) {
		_grid = pool.GetGroup(Matcher.Grid);
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.GridField, Matcher.Position));
		pool.GetGroup(Matcher.Path).OnEntityRemoved += onEntityPathRemoved;
		pool.GetGroup(Matcher.GridField).OnEntityRemoved += onEntityGridRemoved;
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;

		for (int i = 0; i < _group.count; i++) {
			Entity e = _group.GetEntities()[i];
			GridFieldComponent component = e.gridField;
			if (component.state == GridFieldState.IDLE) {
				component.time -= deltaTime;
				if (component.time < 0.0f) {
					component.time = component.freezeDuration;
					addTween(component, e);
				}
			}
		}
	}

	void addTween(GridFieldComponent component, Entity e) {
		PositionComponent position = e.position;
		Entity gridEntity = getGridEntity(component.type);
		GridComponent grid = gridEntity.grid;
		int previousX = component.x;
		int previousY = component.y;
		PositionComponent gridPosition = gridEntity.position;
        findRandomField(grid.grid, GridState.FREE, out component.x, out component.y);
		grid.grid[component.x, component.y] = GridState.BUSY;
		if (previousX != -1) {
			grid.grid[previousX, previousY] = GridState.FREE;
		}
		component.state = GridFieldState.TWEEN;

		e.AddTween(true, new List<Tween>());
        e.tween.AddTween(position, EaseTypes.quadIn, PositionAccessorType.XY, 3.0f)
            .From(position.pos.x, position.pos.y)
            .To(gridPosition.pos.x + (float)component.x * grid.fieldSize, gridPosition.pos.y - (float)component.y * grid.fieldSize)
            .SetEndCallback(onComplete);
    }

	void onEntityPathRemoved(Group group, Entity entity, int index, IComponent component) {
		if (entity.hasGridField) {
			entity.gridField.state = GridFieldState.IDLE;
			entity.velocity.vel.Set(0.0f, 0.0f);
			entity.velocityLimit.maxVelocity = 0.0f;
			entity.isMoveWithCamera = true;
		}
	}

	void onEntityGridRemoved(Group group, Entity entity, int index, IComponent component) {
		GridFieldComponent gridField = (GridFieldComponent)component;
		if (gridField.state == GridFieldState.INACTIVE) {
			return;
		}
		Entity gridEntity = getGridEntity(gridField.type);
		if (gridEntity != null && gridEntity.hasGrid) {
			gridEntity.grid.grid[gridField.x, gridField.y] = GridState.FREE;
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

    void findRandomField(GridState[,] grid, GridState state, out int x, out int y) {
        List<Point2D> freeFields = new List<Point2D>();
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                if (grid[i, j] == state) {
                    freeFields.Add(new Point2D(i, j));
                }
            }
        }
        if (freeFields.Count > 0) {
            Point2D point = freeFields[Random.Range(0, freeFields.Count - 1)];
            x = point.x;
            y = point.y;
        }
        else {
            x = 0;
            y = 0;
        }
    }
}