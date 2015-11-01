using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ParticleSpawnComponent particleSpawn { get { return (ParticleSpawnComponent)GetComponent(ComponentIds.ParticleSpawn); } }

        public bool hasParticleSpawn { get { return HasComponent(ComponentIds.ParticleSpawn); } }

        static readonly Stack<ParticleSpawnComponent> _particleSpawnComponentPool = new Stack<ParticleSpawnComponent>();

        public static void ClearParticleSpawnComponentPool() {
            _particleSpawnComponentPool.Clear();
        }

        public Entity AddParticleSpawn(int newAmount, string newResource, float newVelocity, float newLifespan) {
            var component = _particleSpawnComponentPool.Count > 0 ? _particleSpawnComponentPool.Pop() : new ParticleSpawnComponent();
            component.amount = newAmount;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.lifespan = newLifespan;
            return AddComponent(ComponentIds.ParticleSpawn, component);
        }

        public Entity ReplaceParticleSpawn(int newAmount, string newResource, float newVelocity, float newLifespan) {
            var previousComponent = hasParticleSpawn ? particleSpawn : null;
            var component = _particleSpawnComponentPool.Count > 0 ? _particleSpawnComponentPool.Pop() : new ParticleSpawnComponent();
            component.amount = newAmount;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.lifespan = newLifespan;
            ReplaceComponent(ComponentIds.ParticleSpawn, component);
            if (previousComponent != null) {
                _particleSpawnComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveParticleSpawn() {
            var component = particleSpawn;
            RemoveComponent(ComponentIds.ParticleSpawn);
            _particleSpawnComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherParticleSpawn;

        public static IMatcher ParticleSpawn {
            get {
                if (_matcherParticleSpawn == null) {
                    _matcherParticleSpawn = Matcher.AllOf(ComponentIds.ParticleSpawn);
                }

                return _matcherParticleSpawn;
            }
        }
    }
}
