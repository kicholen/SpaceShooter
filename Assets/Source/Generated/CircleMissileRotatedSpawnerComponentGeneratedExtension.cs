using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CircleMissileRotatedSpawnerComponent circleMissileRotatedSpawner { get { return (CircleMissileRotatedSpawnerComponent)GetComponent(ComponentIds.CircleMissileRotatedSpawner); } }

        public bool hasCircleMissileRotatedSpawner { get { return HasComponent(ComponentIds.CircleMissileRotatedSpawner); } }

        static readonly Stack<CircleMissileRotatedSpawnerComponent> _circleMissileRotatedSpawnerComponentPool = new Stack<CircleMissileRotatedSpawnerComponent>();

        public static void ClearCircleMissileRotatedSpawnerComponentPool() {
            _circleMissileRotatedSpawnerComponentPool.Clear();
        }

        public Entity AddCircleMissileRotatedSpawner(int newAmount, int newWaves, int newAngle, int newAngleOffset, float newTime, float newSpawnDelay, string newResource, float newVelocityX, float newVelocityY, float newVelocityOffsetX, float newVelocityOffsetY, int newCollisionType) {
            var component = _circleMissileRotatedSpawnerComponentPool.Count > 0 ? _circleMissileRotatedSpawnerComponentPool.Pop() : new CircleMissileRotatedSpawnerComponent();
            component.amount = newAmount;
            component.waves = newWaves;
            component.angle = newAngle;
            component.angleOffset = newAngleOffset;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocityX = newVelocityX;
            component.velocityY = newVelocityY;
            component.velocityOffsetX = newVelocityOffsetX;
            component.velocityOffsetY = newVelocityOffsetY;
            component.collisionType = newCollisionType;
            return AddComponent(ComponentIds.CircleMissileRotatedSpawner, component);
        }

        public Entity ReplaceCircleMissileRotatedSpawner(int newAmount, int newWaves, int newAngle, int newAngleOffset, float newTime, float newSpawnDelay, string newResource, float newVelocityX, float newVelocityY, float newVelocityOffsetX, float newVelocityOffsetY, int newCollisionType) {
            var previousComponent = hasCircleMissileRotatedSpawner ? circleMissileRotatedSpawner : null;
            var component = _circleMissileRotatedSpawnerComponentPool.Count > 0 ? _circleMissileRotatedSpawnerComponentPool.Pop() : new CircleMissileRotatedSpawnerComponent();
            component.amount = newAmount;
            component.waves = newWaves;
            component.angle = newAngle;
            component.angleOffset = newAngleOffset;
            component.time = newTime;
            component.spawnDelay = newSpawnDelay;
            component.resource = newResource;
            component.velocityX = newVelocityX;
            component.velocityY = newVelocityY;
            component.velocityOffsetX = newVelocityOffsetX;
            component.velocityOffsetY = newVelocityOffsetY;
            component.collisionType = newCollisionType;
            ReplaceComponent(ComponentIds.CircleMissileRotatedSpawner, component);
            if (previousComponent != null) {
                _circleMissileRotatedSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCircleMissileRotatedSpawner() {
            var component = circleMissileRotatedSpawner;
            RemoveComponent(ComponentIds.CircleMissileRotatedSpawner);
            _circleMissileRotatedSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCircleMissileRotatedSpawner;

        public static IMatcher CircleMissileRotatedSpawner {
            get {
                if (_matcherCircleMissileRotatedSpawner == null) {
                    _matcherCircleMissileRotatedSpawner = Matcher.AllOf(ComponentIds.CircleMissileRotatedSpawner);
                }

                return _matcherCircleMissileRotatedSpawner;
            }
        }
    }
}
