using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public HomeMissileComponent homeMissile { get { return (HomeMissileComponent)GetComponent(ComponentIds.HomeMissile); } }

        public bool hasHomeMissile { get { return HasComponent(ComponentIds.HomeMissile); } }

        static readonly Stack<HomeMissileComponent> _homeMissileComponentPool = new Stack<HomeMissileComponent>();

        public static void ClearHomeMissileComponentPool() {
            _homeMissileComponentPool.Clear();
        }

        public Entity AddHomeMissile(UnityEngine.GameObject newTarget, float newMaxVelocity, float newRandom) {
            var component = _homeMissileComponentPool.Count > 0 ? _homeMissileComponentPool.Pop() : new HomeMissileComponent();
            component.target = newTarget;
            component.maxVelocity = newMaxVelocity;
            component.random = newRandom;
            return AddComponent(ComponentIds.HomeMissile, component);
        }

        public Entity ReplaceHomeMissile(UnityEngine.GameObject newTarget, float newMaxVelocity, float newRandom) {
            var previousComponent = hasHomeMissile ? homeMissile : null;
            var component = _homeMissileComponentPool.Count > 0 ? _homeMissileComponentPool.Pop() : new HomeMissileComponent();
            component.target = newTarget;
            component.maxVelocity = newMaxVelocity;
            component.random = newRandom;
            ReplaceComponent(ComponentIds.HomeMissile, component);
            if (previousComponent != null) {
                _homeMissileComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveHomeMissile() {
            var component = homeMissile;
            RemoveComponent(ComponentIds.HomeMissile);
            _homeMissileComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherHomeMissile;

        public static AllOfMatcher HomeMissile {
            get {
                if (_matcherHomeMissile == null) {
                    _matcherHomeMissile = new Matcher(ComponentIds.HomeMissile);
                }

                return _matcherHomeMissile;
            }
        }
    }
}
