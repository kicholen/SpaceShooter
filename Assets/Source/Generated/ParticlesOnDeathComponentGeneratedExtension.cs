using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ParticlesOnDeathComponent particlesOnDeath { get { return (ParticlesOnDeathComponent)GetComponent(ComponentIds.ParticlesOnDeath); } }

        public bool hasParticlesOnDeath { get { return HasComponent(ComponentIds.ParticlesOnDeath); } }

        static readonly Stack<ParticlesOnDeathComponent> _particlesOnDeathComponentPool = new Stack<ParticlesOnDeathComponent>();

        public static void ClearParticlesOnDeathComponentPool() {
            _particlesOnDeathComponentPool.Clear();
        }

        public Entity AddParticlesOnDeath(int newType) {
            var component = _particlesOnDeathComponentPool.Count > 0 ? _particlesOnDeathComponentPool.Pop() : new ParticlesOnDeathComponent();
            component.type = newType;
            return AddComponent(ComponentIds.ParticlesOnDeath, component);
        }

        public Entity ReplaceParticlesOnDeath(int newType) {
            var previousComponent = hasParticlesOnDeath ? particlesOnDeath : null;
            var component = _particlesOnDeathComponentPool.Count > 0 ? _particlesOnDeathComponentPool.Pop() : new ParticlesOnDeathComponent();
            component.type = newType;
            ReplaceComponent(ComponentIds.ParticlesOnDeath, component);
            if (previousComponent != null) {
                _particlesOnDeathComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveParticlesOnDeath() {
            var component = particlesOnDeath;
            RemoveComponent(ComponentIds.ParticlesOnDeath);
            _particlesOnDeathComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherParticlesOnDeath;

        public static IMatcher ParticlesOnDeath {
            get {
                if (_matcherParticlesOnDeath == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ParticlesOnDeath);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherParticlesOnDeath = matcher;
                }

                return _matcherParticlesOnDeath;
            }
        }
    }
}
