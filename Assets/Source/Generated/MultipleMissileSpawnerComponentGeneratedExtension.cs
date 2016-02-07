using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MultipleMissileSpawnerComponent multipleMissileSpawner { get { return (MultipleMissileSpawnerComponent)GetComponent(ComponentIds.MultipleMissileSpawner); } }

        public bool hasMultipleMissileSpawner { get { return HasComponent(ComponentIds.MultipleMissileSpawner); } }

        static readonly Stack<MultipleMissileSpawnerComponent> _multipleMissileSpawnerComponentPool = new Stack<MultipleMissileSpawnerComponent>();

        public static void ClearMultipleMissileSpawnerComponentPool() {
            _multipleMissileSpawnerComponentPool.Clear();
        }

        public Entity AddMultipleMissileSpawner(int newAmount, int newDamage, int newCurrentAmount, float newTimeDelay, float newDelay, float newTime, float newSpawnDelay, string newResource, float newRandomPositionOffsetX, UnityEngine.Vector2 newStartVelocity, int newCollisionType) {
            var component = _multipleMissileSpawnerComponentPool.Count > 0 ? _multipleMissileSpawnerComponentPool.Pop() : new MultipleMissileSpawnerComponent();
            component.amount = newAmount;
            component.damage = newDamage;
            component.currentAmount = newCurrentAmount;
            component.timeDelay = newTimeDelay;
            component.delay = newDelay;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.randomPositionOffsetX = newRandomPositionOffsetX;
            component.startVelocity = newStartVelocity;
            component.collisionType = newCollisionType;
            return AddComponent(ComponentIds.MultipleMissileSpawner, component);
        }

        public Entity ReplaceMultipleMissileSpawner(int newAmount, int newDamage, int newCurrentAmount, float newTimeDelay, float newDelay, float newTime, float newSpawnDelay, string newResource, float newRandomPositionOffsetX, UnityEngine.Vector2 newStartVelocity, int newCollisionType) {
            var previousComponent = hasMultipleMissileSpawner ? multipleMissileSpawner : null;
            var component = _multipleMissileSpawnerComponentPool.Count > 0 ? _multipleMissileSpawnerComponentPool.Pop() : new MultipleMissileSpawnerComponent();
            component.amount = newAmount;
            component.damage = newDamage;
            component.currentAmount = newCurrentAmount;
            component.timeDelay = newTimeDelay;
            component.delay = newDelay;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.randomPositionOffsetX = newRandomPositionOffsetX;
            component.startVelocity = newStartVelocity;
            component.collisionType = newCollisionType;
            ReplaceComponent(ComponentIds.MultipleMissileSpawner, component);
            if (previousComponent != null) {
                _multipleMissileSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMultipleMissileSpawner() {
            var component = multipleMissileSpawner;
            RemoveComponent(ComponentIds.MultipleMissileSpawner);
            _multipleMissileSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMultipleMissileSpawner;

        public static IMatcher MultipleMissileSpawner {
            get {
                if (_matcherMultipleMissileSpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MultipleMissileSpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMultipleMissileSpawner = matcher;
                }

                return _matcherMultipleMissileSpawner;
            }
        }
    }
}
