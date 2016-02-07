using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MissileSpawnerComponent missileSpawner { get { return (MissileSpawnerComponent)GetComponent(ComponentIds.MissileSpawner); } }

        public bool hasMissileSpawner { get { return HasComponent(ComponentIds.MissileSpawner); } }

        static readonly Stack<MissileSpawnerComponent> _missileSpawnerComponentPool = new Stack<MissileSpawnerComponent>();

        public static void ClearMissileSpawnerComponentPool() {
            _missileSpawnerComponentPool.Clear();
        }

        public Entity AddMissileSpawner(float newTime, int newDamage, float newSpawnDelay, string newResource, UnityEngine.Vector2 newVelocity, int newCollisionType) {
            var component = _missileSpawnerComponentPool.Count > 0 ? _missileSpawnerComponentPool.Pop() : new MissileSpawnerComponent();
            component.time = newTime;
            component.damage = newDamage;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            return AddComponent(ComponentIds.MissileSpawner, component);
        }

        public Entity ReplaceMissileSpawner(float newTime, int newDamage, float newSpawnDelay, string newResource, UnityEngine.Vector2 newVelocity, int newCollisionType) {
            var previousComponent = hasMissileSpawner ? missileSpawner : null;
            var component = _missileSpawnerComponentPool.Count > 0 ? _missileSpawnerComponentPool.Pop() : new MissileSpawnerComponent();
            component.time = newTime;
            component.damage = newDamage;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
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
        static IMatcher _matcherMissileSpawner;

        public static IMatcher MissileSpawner {
            get {
                if (_matcherMissileSpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MissileSpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMissileSpawner = matcher;
                }

                return _matcherMissileSpawner;
            }
        }
    }
}
