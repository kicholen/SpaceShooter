using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public VelocityComponent velocity { get { return (VelocityComponent)GetComponent(ComponentIds.Velocity); } }

        public bool hasVelocity { get { return HasComponent(ComponentIds.Velocity); } }

        static readonly Stack<VelocityComponent> _velocityComponentPool = new Stack<VelocityComponent>();

        public static void ClearVelocityComponentPool() {
            _velocityComponentPool.Clear();
        }

        public Entity AddVelocity(UnityEngine.Vector2 newVel) {
            var component = _velocityComponentPool.Count > 0 ? _velocityComponentPool.Pop() : new VelocityComponent();
            component.vel = newVel;
            return AddComponent(ComponentIds.Velocity, component);
        }

        public Entity ReplaceVelocity(UnityEngine.Vector2 newVel) {
            var previousComponent = hasVelocity ? velocity : null;
            var component = _velocityComponentPool.Count > 0 ? _velocityComponentPool.Pop() : new VelocityComponent();
            component.vel = newVel;
            ReplaceComponent(ComponentIds.Velocity, component);
            if (previousComponent != null) {
                _velocityComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveVelocity() {
            var component = velocity;
            RemoveComponent(ComponentIds.Velocity);
            _velocityComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherVelocity;

        public static IMatcher Velocity {
            get {
                if (_matcherVelocity == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Velocity);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherVelocity = matcher;
                }

                return _matcherVelocity;
            }
        }
    }
}
