using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LaserSpawnerComponent laserSpawner { get { return (LaserSpawnerComponent)GetComponent(ComponentIds.LaserSpawner); } }

        public bool hasLaserSpawner { get { return HasComponent(ComponentIds.LaserSpawner); } }

        static readonly Stack<LaserSpawnerComponent> _laserSpawnerComponentPool = new Stack<LaserSpawnerComponent>();

        public static void ClearLaserSpawnerComponentPool() {
            _laserSpawnerComponentPool.Clear();
        }

        public Entity AddLaserSpawner(float newVelocity, float newHeight, float newAngle, UnityEngine.Vector2 newDirection, int newCollisionType, Entitas.Entity newLaser) {
            var component = _laserSpawnerComponentPool.Count > 0 ? _laserSpawnerComponentPool.Pop() : new LaserSpawnerComponent();
            component.velocity = newVelocity;
            component.height = newHeight;
            component.angle = newAngle;
            component.direction = newDirection;
            component.collisionType = newCollisionType;
            component.laser = newLaser;
            return AddComponent(ComponentIds.LaserSpawner, component);
        }

        public Entity ReplaceLaserSpawner(float newVelocity, float newHeight, float newAngle, UnityEngine.Vector2 newDirection, int newCollisionType, Entitas.Entity newLaser) {
            var previousComponent = hasLaserSpawner ? laserSpawner : null;
            var component = _laserSpawnerComponentPool.Count > 0 ? _laserSpawnerComponentPool.Pop() : new LaserSpawnerComponent();
            component.velocity = newVelocity;
            component.height = newHeight;
            component.angle = newAngle;
            component.direction = newDirection;
            component.collisionType = newCollisionType;
            component.laser = newLaser;
            ReplaceComponent(ComponentIds.LaserSpawner, component);
            if (previousComponent != null) {
                _laserSpawnerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLaserSpawner() {
            var component = laserSpawner;
            RemoveComponent(ComponentIds.LaserSpawner);
            _laserSpawnerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLaserSpawner;

        public static AllOfMatcher LaserSpawner {
            get {
                if (_matcherLaserSpawner == null) {
                    _matcherLaserSpawner = new Matcher(ComponentIds.LaserSpawner);
                }

                return _matcherLaserSpawner;
            }
        }
    }
}
