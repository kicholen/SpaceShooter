using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public TargetMissileSpawnerComponent targetMissileSpawner { get { return (TargetMissileSpawnerComponent)GetComponent(ComponentIds.TargetMissileSpawner); } }

        public bool hasTargetMissileSpawner { get { return HasComponent(ComponentIds.TargetMissileSpawner); } }

        static readonly Stack<TargetMissileSpawnerComponent> _targetMissileSpawnerComponentPool = new Stack<TargetMissileSpawnerComponent>();

        public static void ClearTargetMissileSpawnerComponentPool() {
            _targetMissileSpawnerComponentPool.Clear();
        }

        public Entity AddTargetMissileSpawner(float newTime, int newDamage, float newSpawnDelay, string newResource, float newVelocity, int newCollisionType, int newTargetCollisionType) {
            var component = _targetMissileSpawnerComponentPool.Count > 0 ? _targetMissileSpawnerComponentPool.Pop() : new TargetMissileSpawnerComponent();
            component.time = newTime;
            component.damage = newDamage;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            component.targetCollisionType = newTargetCollisionType;
            return AddComponent(ComponentIds.TargetMissileSpawner, component);
        }

        public Entity ReplaceTargetMissileSpawner(float newTime, int newDamage, float newSpawnDelay, string newResource, float newVelocity, int newCollisionType, int newTargetCollisionType) {
            var previousComponent = hasTargetMissileSpawner ? targetMissileSpawner : null;
            var component = _targetMissileSpawnerComponentPool.Count > 0 ? _targetMissileSpawnerComponentPool.Pop() : new TargetMissileSpawnerComponent();
            component.time = newTime;
            component.damage = newDamage;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            component.targetCollisionType = newTargetCollisionType;
            ReplaceComponent(ComponentIds.TargetMissileSpawner, component);
            if (previousComponent != null) {
                _targetMissileSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTargetMissileSpawner() {
            var component = targetMissileSpawner;
            RemoveComponent(ComponentIds.TargetMissileSpawner);
            _targetMissileSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTargetMissileSpawner;

        public static IMatcher TargetMissileSpawner {
            get {
                if (_matcherTargetMissileSpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TargetMissileSpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTargetMissileSpawner = matcher;
                }

                return _matcherTargetMissileSpawner;
            }
        }
    }
}
