using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public HomeMissileSpawnerComponent homeMissileSpawner { get { return (HomeMissileSpawnerComponent)GetComponent(ComponentIds.HomeMissileSpawner); } }

        public bool hasHomeMissileSpawner { get { return HasComponent(ComponentIds.HomeMissileSpawner); } }

        static readonly Stack<HomeMissileSpawnerComponent> _homeMissileSpawnerComponentPool = new Stack<HomeMissileSpawnerComponent>();

        public static void ClearHomeMissileSpawnerComponentPool() {
            _homeMissileSpawnerComponentPool.Clear();
        }

        public Entity AddHomeMissileSpawner(float newTime, float newSpawnDelay, string newResource, float newVelocity, int newCollisionType) {
            var component = _homeMissileSpawnerComponentPool.Count > 0 ? _homeMissileSpawnerComponentPool.Pop() : new HomeMissileSpawnerComponent();
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            return AddComponent(ComponentIds.HomeMissileSpawner, component);
        }

        public Entity ReplaceHomeMissileSpawner(float newTime, float newSpawnDelay, string newResource, float newVelocity, int newCollisionType) {
            var previousComponent = hasHomeMissileSpawner ? homeMissileSpawner : null;
            var component = _homeMissileSpawnerComponentPool.Count > 0 ? _homeMissileSpawnerComponentPool.Pop() : new HomeMissileSpawnerComponent();
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            ReplaceComponent(ComponentIds.HomeMissileSpawner, component);
            if (previousComponent != null) {
                _homeMissileSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveHomeMissileSpawner() {
            var component = homeMissileSpawner;
            RemoveComponent(ComponentIds.HomeMissileSpawner);
            _homeMissileSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherHomeMissileSpawner;

        public static AllOfMatcher HomeMissileSpawner {
            get {
                if (_matcherHomeMissileSpawner == null) {
                    _matcherHomeMissileSpawner = new Matcher(ComponentIds.HomeMissileSpawner);
                }

                return _matcherHomeMissileSpawner;
            }
        }
    }
}
