using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public VelocityLimitComponent velocityLimit { get { return (VelocityLimitComponent)GetComponent(ComponentIds.VelocityLimit); } }

        public bool hasVelocityLimit { get { return HasComponent(ComponentIds.VelocityLimit); } }

        static readonly Stack<VelocityLimitComponent> _velocityLimitComponentPool = new Stack<VelocityLimitComponent>();

        public static void ClearVelocityLimitComponentPool() {
            _velocityLimitComponentPool.Clear();
        }

        public Entity AddVelocityLimit(float newMaxVelocity) {
            var component = _velocityLimitComponentPool.Count > 0 ? _velocityLimitComponentPool.Pop() : new VelocityLimitComponent();
            component.maxVelocity = newMaxVelocity;
            return AddComponent(ComponentIds.VelocityLimit, component);
        }

        public Entity ReplaceVelocityLimit(float newMaxVelocity) {
            var previousComponent = hasVelocityLimit ? velocityLimit : null;
            var component = _velocityLimitComponentPool.Count > 0 ? _velocityLimitComponentPool.Pop() : new VelocityLimitComponent();
            component.maxVelocity = newMaxVelocity;
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
        static IMatcher _matcherVelocityLimit;

        public static IMatcher VelocityLimit {
            get {
                if (_matcherVelocityLimit == null) {
                    _matcherVelocityLimit = Matcher.AllOf(ComponentIds.VelocityLimit);
                }

                return _matcherVelocityLimit;
            }
        }
    }
}
