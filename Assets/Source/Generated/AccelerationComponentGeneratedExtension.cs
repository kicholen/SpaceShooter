using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AccelerationComponent acceleration { get { return (AccelerationComponent)GetComponent(ComponentIds.Acceleration); } }

        public bool hasAcceleration { get { return HasComponent(ComponentIds.Acceleration); } }

        static readonly Stack<AccelerationComponent> _accelerationComponentPool = new Stack<AccelerationComponent>();

        public static void ClearAccelerationComponentPool() {
            _accelerationComponentPool.Clear();
        }

        public Entity AddAcceleration(float newX, float newY, float newFrictionX, float newFrictionY, bool newStopNearZero) {
            var component = _accelerationComponentPool.Count > 0 ? _accelerationComponentPool.Pop() : new AccelerationComponent();
            component.x = newX;
            component.y = newY;
            component.frictionX = newFrictionX;
            component.frictionY = newFrictionY;
            component.stopNearZero = newStopNearZero;
            return AddComponent(ComponentIds.Acceleration, component);
        }

        public Entity ReplaceAcceleration(float newX, float newY, float newFrictionX, float newFrictionY, bool newStopNearZero) {
            var previousComponent = hasAcceleration ? acceleration : null;
            var component = _accelerationComponentPool.Count > 0 ? _accelerationComponentPool.Pop() : new AccelerationComponent();
            component.x = newX;
            component.y = newY;
            component.frictionX = newFrictionX;
            component.frictionY = newFrictionY;
            component.stopNearZero = newStopNearZero;
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
        static IMatcher _matcherAcceleration;

        public static IMatcher Acceleration {
            get {
                if (_matcherAcceleration == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Acceleration);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherAcceleration = matcher;
                }

                return _matcherAcceleration;
            }
        }
    }
}
