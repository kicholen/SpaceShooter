using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MissileSpawnerComponent missileSpawner { get { return (MissileSpawnerComponent)GetComponent(ComponentIds.MissileSpawner); } }

        public bool hasMissileSpawner { get { return HasComponent(ComponentIds.MissileSpawner); } }

        static readonly Stack<MissileSpawnerComponent> _missileSpawnerComponentPool = new Stack<MissileSpawnerComponent>();

        public static void ClearMissileSpawnerComponentPool() {
            _missileSpawnerComponentPool.Clear();
        }

        public Entity AddMissileSpawner(float newTime, float newSpawnDelay, string newResource, int newCollisionType, float newVelocityX, float newVelocityY, bool newIsFriendly) {
            var component = _missileSpawnerComponentPool.Count > 0 ? _missileSpawnerComponentPool.Pop() : new MissileSpawnerComponent();
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.collisionType = newCollisionType;
            component.velocityX = newVelocityX;
            component.velocityY = newVelocityY;
            component.isFriendly = newIsFriendly;
            return AddComponent(ComponentIds.MissileSpawner, component);
        }

        public Entity ReplaceMissileSpawner(float newTime, float newSpawnDelay, string newResource, int newCollisionType, float newVelocityX, float newVelocityY, bool newIsFriendly) {
            var previousComponent = hasMissileSpawner ? missileSpawner : null;
            var component = _missileSpawnerComponentPool.Count > 0 ? _missileSpawnerComponentPool.Pop() : new MissileSpawnerComponent();
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.collisionType = newCollisionType;
            component.velocityX = newVelocityX;
            component.velocityY = newVelocityY;
            component.isFriendly = newIsFriendly;
            ReplaceComponent(ComponentIds.MissileSpawner, component);
            if (previousComponent != null) {
                _missileSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMissileSpawner() {
            var component = missileSpawner;
            RemoveComponent(ComponentIds.MissileSpawner);
            _missileSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherMissileSpawner;

        public static AllOfMatcher MissileSpawner {
            get {
                if (_matcherMissileSpawner == null) {
                    _matcherMissileSpawner = new Matcher(ComponentIds.MissileSpawner);
                }

                return _matcherMissileSpawner;
            }
        }
    }
}
