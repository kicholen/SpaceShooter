using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GridFieldComponent gridField { get { return (GridFieldComponent)GetComponent(ComponentIds.GridField); } }

        public bool hasGridField { get { return HasComponent(ComponentIds.GridField); } }

        static readonly Stack<GridFieldComponent> _gridFieldComponentPool = new Stack<GridFieldComponent>();

        public static void ClearGridFieldComponentPool() {
            _gridFieldComponentPool.Clear();
        }

        public Entity AddGridField(float newTime, float newFreezeDuration, GridFieldState newState, int newType, int newX, int newY) {
            var component = _gridFieldComponentPool.Count > 0 ? _gridFieldComponentPool.Pop() : new GridFieldComponent();
            component.time = newTime;
            component.freezeDuration = newFreezeDuration;
            component.state = newState;
            component.type = newType;
            component.x = newX;
            component.y = newY;
            return AddComponent(ComponentIds.GridField, component);
        }

        public Entity ReplaceGridField(float newTime, float newFreezeDuration, GridFieldState newState, int newType, int newX, int newY) {
            var previousComponent = hasGridField ? gridField : null;
            var component = _gridFieldComponentPool.Count > 0 ? _gridFieldComponentPool.Pop() : new GridFieldComponent();
            component.time = newTime;
            component.freezeDuration = newFreezeDuration;
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
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.GridField);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherGridField = matcher;
                }

                return _matcherGridField;
            }
        }
    }
}
