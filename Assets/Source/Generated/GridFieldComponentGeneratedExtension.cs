using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GridFieldComponent gridField { get { return (GridFieldComponent)GetComponent(ComponentIds.GridField); } }

        public bool hasGridField { get { return HasComponent(ComponentIds.GridField); } }

        static readonly Stack<GridFieldComponent> _gridFieldComponentPool = new Stack<GridFieldComponent>();

        public static void ClearGridFieldComponentPool() {
            _gridFieldComponentPool.Clear();
        }

        public Entity AddGridField(GridFieldState newState, string newType, int newX, int newY) {
            var component = _gridFieldComponentPool.Count > 0 ? _gridFieldComponentPool.Pop() : new GridFieldComponent();
            component.state = newState;
            component.type = newType;
            component.x = newX;
            component.y = newY;
            return AddComponent(ComponentIds.GridField, component);
        }

        public Entity ReplaceGridField(GridFieldState newState, string newType, int newX, int newY) {
            var previousComponent = hasGridField ? gridField : null;
            var component = _gridFieldComponentPool.Count > 0 ? _gridFieldComponentPool.Pop() : new GridFieldComponent();
            component.state = newState;
            component.type = newType;
            component.x = newX;
            component.y = newY;
            ReplaceComponent(ComponentIds.GridField, component);
            if (previousComponent != null) {
                _gridFieldComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveGridField() {
            var component = gridField;
            RemoveComponent(ComponentIds.GridField);
            _gridFieldComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGridField;

        public static IMatcher GridField {
            get {
                if (_matcherGridField == null) {
                    _matcherGridField = Matcher.AllOf(ComponentIds.GridField);
                }

                return _matcherGridField;
            }
        }
    }
}
