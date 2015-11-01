using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SnapPositionComponent snapPosition { get { return (SnapPositionComponent)GetComponent(ComponentIds.SnapPosition); } }

        public bool hasSnapPosition { get { return HasComponent(ComponentIds.SnapPosition); } }

        static readonly Stack<SnapPositionComponent> _snapPositionComponentPool = new Stack<SnapPositionComponent>();

        public static void ClearSnapPositionComponentPool() {
            _snapPositionComponentPool.Clear();
        }

        public Entity AddSnapPosition(float newX, float newY, float newWidth, float newHeight, bool newShouldSnapToCameraY) {
            var component = _snapPositionComponentPool.Count > 0 ? _snapPositionComponentPool.Pop() : new SnapPositionComponent();
            component.x = newX;
            component.y = newY;
            component.width = newWidth;
            component.height = newHeight;
            component.shouldSnapToCameraY = newShouldSnapToCameraY;
            return AddComponent(ComponentIds.SnapPosition, component);
        }

        public Entity ReplaceSnapPosition(float newX, float newY, float newWidth, float newHeight, bool newShouldSnapToCameraY) {
            var previousComponent = hasSnapPosition ? snapPosition : null;
            var component = _snapPositionComponentPool.Count > 0 ? _snapPositionComponentPool.Pop() : new SnapPositionComponent();
            component.x = newX;
            component.y = newY;
            component.width = newWidth;
            component.height = newHeight;
            component.shouldSnapToCameraY = newShouldSnapToCameraY;
            ReplaceComponent(ComponentIds.SnapPosition, component);
            if (previousComponent != null) {
                _snapPositionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSnapPosition() {
            var component = snapPosition;
            RemoveComponent(ComponentIds.SnapPosition);
            _snapPositionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSnapPosition;

        public static IMatcher SnapPosition {
            get {
                if (_matcherSnapPosition == null) {
                    _matcherSnapPosition = Matcher.AllOf(ComponentIds.SnapPosition);
                }

                return _matcherSnapPosition;
            }
        }
    }
}
