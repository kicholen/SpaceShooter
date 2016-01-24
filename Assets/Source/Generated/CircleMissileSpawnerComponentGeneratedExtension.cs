using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CircleMissileSpawnerComponent circleMissileSpawner { get { return (CircleMissileSpawnerComponent)GetComponent(ComponentIds.CircleMissileSpawner); } }

        public bool hasCircleMissileSpawner { get { return HasComponent(ComponentIds.CircleMissileSpawner); } }

        static readonly Stack<CircleMissileSpawnerComponent> _circleMissileSpawnerComponentPool = new Stack<CircleMissileSpawnerComponent>();

        public static void ClearCircleMissileSpawnerComponentPool() {
            _circleMissileSpawnerComponentPool.Clear();
        }

        public Entity AddCircleMissileSpawner(int newAmount, int newDamage, float newTime, float newSpawnDelay, string newResource, float newVelocity, int newCollisionType) {
            var component = _circleMissileSpawnerComponentPool.Count > 0 ? _circleMissileSpawnerComponentPool.Pop() : new CircleMissileSpawnerComponent();
            component.amount = newAmount;
            component.damage = newDamage;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            return AddComponent(ComponentIds.CircleMissileSpawner, component);
        }

        public Entity ReplaceCircleMissileSpawner(int newAmount, int newDamage, float newTime, float newSpawnDelay, string newResource, float newVelocity, int newCollisionType) {
            var previousComponent = hasCircleMissileSpawner ? circleMissileSpawner : null;
            var component = _circleMissileSpawnerComponentPool.Count > 0 ? _circleMissileSpawnerComponentPool.Pop() : new CircleMissileSpawnerComponent();
            component.amount = newAmount;
            component.damage = newDamage;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocity = newVelocity;
            component.collisionType = newCollisionType;
            ReplaceComponent(ComponentIds.CircleMissileSpawner, component);
            if (previousComponent != null) {
                _circleMissileSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCircleMissileSpawner() {
            var component = circleMissileSpawner;
            RemoveComponent(ComponentIds.CircleMissileSpawner);
            _circleMissileSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCircleMissileSpawner;

        public static IMatcher CircleMissileSpawner {
            get {
                if (_matcherCircleMissileSpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CircleMissileSpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCircleMissileSpawner = matcher;
                }

                return _matcherCircleMissileSpawner;
            }
        }
    }
}
