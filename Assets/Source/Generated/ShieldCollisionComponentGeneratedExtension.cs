using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ShieldCollisionComponent shieldCollision { get { return (ShieldCollisionComponent)GetComponent(ComponentIds.ShieldCollision); } }

        public bool hasShieldCollision { get { return HasComponent(ComponentIds.ShieldCollision); } }

        static readonly Stack<ShieldCollisionComponent> _shieldCollisionComponentPool = new Stack<ShieldCollisionComponent>();

        public static void ClearShieldCollisionComponentPool() {
            _shieldCollisionComponentPool.Clear();
        }

        public Entity AddShieldCollision(float newTime, float newDuration, System.Collections.Generic.Queue<UnityEngine.Vector2> newCollisionsPosition) {
            var component = _shieldCollisionComponentPool.Count > 0 ? _shieldCollisionComponentPool.Pop() : new ShieldCollisionComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.collisionsPosition = newCollisionsPosition;
            return AddComponent(ComponentIds.ShieldCollision, component);
        }

        public Entity ReplaceShieldCollision(float newTime, float newDuration, System.Collections.Generic.Queue<UnityEngine.Vector2> newCollisionsPosition) {
            var previousComponent = hasShieldCollision ? shieldCollision : null;
            var component = _shieldCollisionComponentPool.Count > 0 ? _shieldCollisionComponentPool.Pop() : new ShieldCollisionComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.collisionsPosition = newCollisionsPosition;
            ReplaceComponent(ComponentIds.ShieldCollision, component);
            if (previousComponent != null) {
                _shieldCollisionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveShieldCollision() {
            var component = shieldCollision;
            RemoveComponent(ComponentIds.ShieldCollision);
            _shieldCollisionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShieldCollision;

        public static IMatcher ShieldCollision {
            get {
                if (_matcherShieldCollision == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ShieldCollision);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherShieldCollision = matcher;
                }

                return _matcherShieldCollision;
            }
        }
    }
}
