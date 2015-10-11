using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public VelocityLimitComponent velocityLimit { get { return (VelocityLimitComponent)GetComponent(ComponentIds.VelocityLimit); } }

        public bool hasVelocityLimit { get { return HasComponent(ComponentIds.VelocityLimit); } }

        static readonly Stack<VelocityLimitComponent> _velocityLimitComponentPool = new Stack<VelocityLimitComponent>();

        public static void ClearVelocityLimitComponentPool() {
            _velocityLimitComponentPool.Clear();
        }

        public Entity AddVelocityLimit(float newX, float newY, float newOffsetX, float newOffsetY) {
            var component = _velocityLimitComponentPool.Count > 0 ? _velocityLimitComponentPool.Pop() : new VelocityLimitComponent();
            component.x = newX;
            component.y = newY;
            component.offsetX = newOffsetX;
            component.offsetY = newOffsetY;
            return AddComponent(ComponentIds.VelocityLimit, component);
        }

        public Entity ReplaceVelocityLimit(float newX, float newY, float newOffsetX, float newOffsetY) {
            var previousComponent = hasVelocityLimit ? velocityLimit : null;
            var component = _velocityLimitComponentPool.Count > 0 ? _velocityLimitComponentPool.Pop() : new VelocityLimitComponent();
            component.x = newX;
            component.y = newY;
            component.offsetX = newOffsetX;
            component.offsetY = newOffsetY;
            ReplaceComponent(ComponentIds.VelocityLimit, component);
            if (previousComponent != null) {
                _velocityLimitComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveVelocityLimit() {
            var component = velocityLimit;
            RemoveComponent(ComponentIds.VelocityLimit);
            _velocityLimitComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherVelocityLimit;

        public static AllOfMatcher VelocityLimit {
            get {
                if (_matcherVelocityLimit == null) {
                    _matcherVelocityLimit = new Matcher(ComponentIds.VelocityLimit);
                }

                return _matcherVelocityLimit;
            }
        }
    }
}
