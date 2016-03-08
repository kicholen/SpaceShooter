//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public LaserSpawnerComponent laserSpawner { get { return (LaserSpawnerComponent)GetComponent(ComponentIds.LaserSpawner); } }

        public bool hasLaserSpawner { get { return HasComponent(ComponentIds.LaserSpawner); } }

        public Entity AddLaserSpawner(float newVelocity, float newHeight, float newMaxHeight, float newAngle, UnityEngine.Vector2 newDirection, int newCollisionType, int newDamage, string newResource, Entitas.Entity newLaser) {
            var component = CreateComponent<LaserSpawnerComponent>(ComponentIds.LaserSpawner);
            component.velocity = newVelocity;
            component.height = newHeight;
            component.maxHeight = newMaxHeight;
            component.angle = newAngle;
            component.direction = newDirection;
            component.collisionType = newCollisionType;
            component.damage = newDamage;
            component.resource = newResource;
            component.laser = newLaser;
            return AddComponent(ComponentIds.LaserSpawner, component);
        }

        public Entity ReplaceLaserSpawner(float newVelocity, float newHeight, float newMaxHeight, float newAngle, UnityEngine.Vector2 newDirection, int newCollisionType, int newDamage, string newResource, Entitas.Entity newLaser) {
            var component = CreateComponent<LaserSpawnerComponent>(ComponentIds.LaserSpawner);
            component.velocity = newVelocity;
            component.height = newHeight;
            component.maxHeight = newMaxHeight;
            component.angle = newAngle;
            component.direction = newDirection;
            component.collisionType = newCollisionType;
            component.damage = newDamage;
            component.resource = newResource;
            component.laser = newLaser;
            ReplaceComponent(ComponentIds.LaserSpawner, component);
            return this;
        }

        public Entity RemoveLaserSpawner() {
            return RemoveComponent(ComponentIds.LaserSpawner);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherLaserSpawner;

        public static IMatcher LaserSpawner {
            get {
                if (_matcherLaserSpawner == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.LaserSpawner);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherLaserSpawner = matcher;
                }

                return _matcherLaserSpawner;
            }
        }
    }
}
