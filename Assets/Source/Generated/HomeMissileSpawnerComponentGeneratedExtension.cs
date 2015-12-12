using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public HomeMissileSpawnerComponent homeMissileSpawner { get { return (HomeMissileSpawnerComponent)GetComponent(ComponentIds.HomeMissileSpawner); } }

        public bool hasHomeMissileSpawner { get { return HasComponent(ComponentIds.HomeMissileSpawner); } }

        static readonly Stack<HomeMissileSpawnerComponent> _homeMissileSpawnerComponentPool = new Stack<HomeMissileSpawnerComponent>();

        public static void ClearHomeMissileSpawnerComponentPool() {
            _homeMissileSpawnerComponentPool.Clear();
        }

        public Entity AddHomeMissileSpawner(float newTime, float newSpawnDelay, int newDamage, string newResource, float newVelocity, int newOwnerCollisionType) {
            var component = _homeMissileSpawnerComponentPool.Count > 0 ? _homeMissileSpawnerComponentPool.Pop() : new HomeMissileSpawnerComponent();
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.damage = newDamage;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.ownerCollisionType = newOwnerCollisionType;
            return AddComponent(ComponentIds.HomeMissileSpawner, component);
        }

        public Entity ReplaceHomeMissileSpawner(float newTime, float newSpawnDelay, int newDamage, string newResource, float newVelocity, int newOwnerCollisionType) {
            var previousComponent = hasHomeMissileSpawner ? homeMissileSpawner : null;
            var component = _homeMissileSpawnerComponentPool.Count > 0 ? _homeMissileSpawnerComponentPool.Pop() : new HomeMissileSpawnerComponent();
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.damage = newDamage;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.ownerCollisionType = newOwnerCollisionType;
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
        static IMatcher _matcherHomeMissileSpawner;

        public static IMatcher HomeMissileSpawner {
            get {
                if (_matcherHomeMissileSpawner == null) {
                    _matcherHomeMissileSpawner = Matcher.AllOf(ComponentIds.HomeMissileSpawner);
                }

                return _matcherHomeMissileSpawner;
            }
        }
    }
}
