using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public RelativePositionComponent relativePosition { get { return (RelativePositionComponent)GetComponent(ComponentIds.RelativePosition); } }

        public bool hasRelativePosition { get { return HasComponent(ComponentIds.RelativePosition); } }

        static readonly Stack<RelativePositionComponent> _relativePositionComponentPool = new Stack<RelativePositionComponent>();

        public static void ClearRelativePositionComponentPool() {
            _relativePositionComponentPool.Clear();
        }

        public Entity AddRelativePosition(float newX, float newY) {
            var component = _relativePositionComponentPool.Count > 0 ? _relativePositionComponentPool.Pop() : new RelativePositionComponent();
            component.x = newX;
            component.y = newY;
            return AddComponent(ComponentIds.RelativePosition, component);
        }

        public Entity ReplaceRelativePosition(float newX, float newY) {
            var previousComponent = hasRelativePosition ? relativePosition : null;
            var component = _relativePositionComponentPool.Count > 0 ? _relativePositionComponentPool.Pop() : new RelativePositionComponent();
            component.x = newX;
            component.y = newY;
            ReplaceComponent(ComponentIds.RelativePosition, component);
            if (previousComponent != null) {
                _relativePositionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRelativePosition() {
            var component = relativePosition;
            RemoveComponent(ComponentIds.RelativePosition);
            _relativePositionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherRelativePosition;

        public static AllOfMatcher RelativePosition {
            get {
                if (_matcherRelativePosition == null) {
                    _matcherRelativePosition = new Matcher(ComponentIds.RelativePosition);
                }

                return _matcherRelativePosition;
            }
        }
    }
}
