using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GridComponent grid { get { return (GridComponent)GetComponent(ComponentIds.Grid); } }

        public bool hasGrid { get { return HasComponent(ComponentIds.Grid); } }

        static readonly Stack<GridComponent> _gridComponentPool = new Stack<GridComponent>();

        public static void ClearGridComponentPool() {
            _gridComponentPool.Clear();
        }

        public Entity AddGrid(string newType, int newSizeX, int newSizeY, GridState[,] newGrid) {
            var component = _gridComponentPool.Count > 0 ? _gridComponentPool.Pop() : new GridComponent();
            component.type = newType;
            component.sizeX = newSizeX;
            component.sizeY = newSizeY;
            component.grid = newGrid;
            return AddComponent(ComponentIds.Grid, component);
        }

        public Entity ReplaceGrid(string newType, int newSizeX, int newSizeY, GridState[,] newGrid) {
            var previousComponent = hasGrid ? grid : null;
            var component = _gridComponentPool.Count > 0 ? _gridComponentPool.Pop() : new GridComponent();
            component.type = newType;
            component.sizeX = newSizeX;
            component.sizeY = newSizeY;
            component.grid = newGrid;
            ReplaceComponent(ComponentIds.Grid, component);
            if (previousComponent != null) {
                _gridComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveGrid() {
            var component = grid;
            RemoveComponent(ComponentIds.Grid);
            _gridComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGrid;

        public static IMatcher Grid {
            get {
                if (_matcherGrid == null) {
                    _matcherGrid = Matcher.AllOf(ComponentIds.Grid);
                }

                return _matcherGrid;
            }
        }
    }
}
