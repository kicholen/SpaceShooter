using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DispersionMissileSpawnerComponent dispersionMissileSpawner { get { return (DispersionMissileSpawnerComponent)GetComponent(ComponentIds.DispersionMissileSpawner); } }

        public bool hasDispersionMissileSpawner { get { return HasComponent(ComponentIds.DispersionMissileSpawner); } }

        static readonly Stack<DispersionMissileSpawnerComponent> _dispersionMissileSpawnerComponentPool = new Stack<DispersionMissileSpawnerComponent>();

        public static void ClearDispersionMissileSpawnerComponentPool() {
            _dispersionMissileSpawnerComponentPool.Clear();
        }

        public Entity AddDispersionMissileSpawner(float newTime, int newDamage, float newSpawnDelay, float newAngle, string newResource, float newVelocity, int newCollisionType) {
            var component = _dispersionMissileSpawnerComponentPool.Count > 0 ? _dispersionMissileSpawnerComponentPool.Pop() : new DispersionMissileSpawnerComponent();
            component.time = newTime;
            component.damage = newDamage;
            component.spawnDelay = newSpawnDelay;
            component.angle = newAngle;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            return AddComponent(ComponentIds.DispersionMissileSpawner, component);
        }

        public Entity ReplaceDispersionMissileSpawner(float newTime, int newDamage, float newSpawnDelay, float newAngle, string newResource, float newVelocity, int newCollisionType) {
            var previousComponent = hasDispersionMissileSpawner ? dispersionMissileSpawner : null;
            var component = _dispersionMissileSpawnerComponentPool.Count > 0 ? _dispersionMissileSpawnerComponentPool.Pop() : new DispersionMissileSpawnerComponent();
            component.time = newTime;
            component.damage = newDamage;
            component.spawnDelay = newSpawnDelay;
            component.angle = newAngle;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            ReplaceComponent(ComponentIds.DispersionMissileSpawner, component);
            if (previousComponent != null) {
                _dispersionMissileSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDispersionMissileSpawner() {
            var component = dispersionMissileSpawner;
            RemoveComponent(ComponentIds.DispersionMissileSpawner);
            _dispersionMissileSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDispersionMissileSpawner;

        public static IMatcher DispersionMissileSpawner {
            get {
                if (_matcherDispersionMissileSpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.DispersionMissileSpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDispersionMissileSpawner = matcher;
                }

                return _matcherDispersionMissileSpawner;
            }
        }
    }
}
