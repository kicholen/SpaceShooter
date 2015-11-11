using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ExplosionOnDeathComponent explosionOnDeath { get { return (ExplosionOnDeathComponent)GetComponent(ComponentIds.ExplosionOnDeath); } }

        public bool hasExplosionOnDeath { get { return HasComponent(ComponentIds.ExplosionOnDeath); } }

        static readonly Stack<ExplosionOnDeathComponent> _explosionOnDeathComponentPool = new Stack<ExplosionOnDeathComponent>();

        public static void ClearExplosionOnDeathComponentPool() {
            _explosionOnDeathComponentPool.Clear();
        }

        public Entity AddExplosionOnDeath(float newLifetime, string newResource) {
            var component = _explosionOnDeathComponentPool.Count > 0 ? _explosionOnDeathComponentPool.Pop() : new ExplosionOnDeathComponent();
            component.lifetime = newLifetime;
            component.resource = newResource;
            return AddComponent(ComponentIds.ExplosionOnDeath, component);
        }

        public Entity ReplaceExplosionOnDeath(float newLifetime, string newResource) {
            var previousComponent = hasExplosionOnDeath ? explosionOnDeath : null;
            var component = _explosionOnDeathComponentPool.Count > 0 ? _explosionOnDeathComponentPool.Pop() : new ExplosionOnDeathComponent();
            component.lifetime = newLifetime;
            component.resource = newResource;
            ReplaceComponent(ComponentIds.ExplosionOnDeath, component);
            if (previousComponent != null) {
                _explosionOnDeathComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveExplosionOnDeath() {
            var component = explosionOnDeath;
            RemoveComponent(ComponentIds.ExplosionOnDeath);
            _explosionOnDeathComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherExplosionOnDeath;

        public static IMatcher ExplosionOnDeath {
            get {
                if (_matcherExplosionOnDeath == null) {
                    _matcherExplosionOnDeath = Matcher.AllOf(ComponentIds.ExplosionOnDeath);
                }

                return _matcherExplosionOnDeath;
            }
        }
    }
}
