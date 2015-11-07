using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public HomeMissileComponent homeMissile { get { return (HomeMissileComponent)GetComponent(ComponentIds.HomeMissile); } }

        public bool hasHomeMissile { get { return HasComponent(ComponentIds.HomeMissile); } }

        static readonly Stack<HomeMissileComponent> _homeMissileComponentPool = new Stack<HomeMissileComponent>();

        public static void ClearHomeMissileComponentPool() {
            _homeMissileComponentPool.Clear();
        }

        public Entity AddHomeMissile(float newRandom, int newTargetCollisionType) {
            var component = _homeMissileComponentPool.Count > 0 ? _homeMissileComponentPool.Pop() : new HomeMissileComponent();
            component.random = newRandom;
            component.targetCollisionType = newTargetCollisionType;
            return AddComponent(ComponentIds.HomeMissile, component);
        }

        public Entity ReplaceHomeMissile(float newRandom, int newTargetCollisionType) {
            var previousComponent = hasHomeMissile ? homeMissile : null;
            var component = _homeMissileComponentPool.Count > 0 ? _homeMissileComponentPool.Pop() : new HomeMissileComponent();
            component.random = newRandom;
            component.targetCollisionType = newTargetCollisionType;
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
        static IMatcher _matcherHomeMissile;

        public static IMatcher HomeMissile {
            get {
                if (_matcherHomeMissile == null) {
                    _matcherHomeMissile = Matcher.AllOf(ComponentIds.HomeMissile);
                }

                return _matcherHomeMissile;
            }
        }
    }
}
