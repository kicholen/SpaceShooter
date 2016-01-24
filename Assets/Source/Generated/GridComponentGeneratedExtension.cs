using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GridComponent grid { get { return (GridComponent)GetComponent(ComponentIds.Grid); } }

        public bool hasGrid { get { return HasComponent(ComponentIds.Grid); } }

        static readonly Stack<GridComponent> _gridComponentPool = new Stack<GridComponent>();

        public static void ClearGridComponentPool() {
            _gridComponentPool.Clear();
        }

        public Entity AddGrid(int newType, float newFieldSize, GridState[,] newGrid) {
            var component = _gridComponentPool.Count > 0 ? _gridComponentPool.Pop() : new GridComponent();
            component.type = newType;
            component.fieldSize = newFieldSize;
            component.grid = newGrid;
            return AddComponent(ComponentIds.Grid, component);
        }

        public Entity ReplaceGrid(int newType, float newFieldSize, GridState[,] newGrid) {
            var previousComponent = hasGrid ? grid : null;
            var component = _gridComponentPool.Count > 0 ? _gridComponentPool.Pop() : new GridComponent();
            component.type = newType;
            component.fieldSize = newFieldSize;
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
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Grid);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherGrid = matcher;
                }

                return _matcherGrid;
            }
        }
    }
}
