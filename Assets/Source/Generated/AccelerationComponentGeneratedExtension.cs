using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AccelerationComponent acceleration { get { return (AccelerationComponent)GetComponent(ComponentIds.Acceleration); } }

        public bool hasAcceleration { get { return HasComponent(ComponentIds.Acceleration); } }

        static readonly Stack<AccelerationComponent> _accelerationComponentPool = new Stack<AccelerationComponent>();

        public static void ClearAccelerationComponentPool() {
            _accelerationComponentPool.Clear();
        }

        public Entity AddAcceleration(float newX, float newY, float newFrictionX, float newFrictionY) {
            var component = _accelerationComponentPool.Count > 0 ? _accelerationComponentPool.Pop() : new AccelerationComponent();
            component.x = newX;
            component.y = newY;
            component.frictionX = newFrictionX;
            component.frictionY = newFrictionY;
            return AddComponent(ComponentIds.Acceleration, component);
        }

        public Entity ReplaceAcceleration(float newX, float newY, float newFrictionX, float newFrictionY) {
            var previousComponent = hasAcceleration ? acceleration : null;
            var component = _accelerationComponentPool.Count > 0 ? _accelerationComponentPool.Pop() : new AccelerationComponent();
            component.x = newX;
            component.y = newY;
            component.frictionX = newFrictionX;
            component.frictionY = newFrictionY;
            ReplaceComponent(ComponentIds.Acceleration, component);
            if (previousComponent != null) {
                _accelerationComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAcceleration() {
            var component = acceleration;
            RemoveComponent(ComponentIds.Acceleration);
            _accelerationComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAcceleration;

        public static AllOfMatcher Acceleration {
            get {
                if (_matcherAcceleration == null) {
                    _matcherAcceleration = new Matcher(ComponentIds.Acceleration);
                }

                return _matcherAcceleration;
            }
        }
    }
}
