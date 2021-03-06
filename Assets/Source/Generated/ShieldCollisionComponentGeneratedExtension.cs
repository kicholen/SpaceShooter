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
        public ShieldCollisionComponent shieldCollision { get { return (ShieldCollisionComponent)GetComponent(ComponentIds.ShieldCollision); } }

        public bool hasShieldCollision { get { return HasComponent(ComponentIds.ShieldCollision); } }

        public Entity AddShieldCollision(float newTime, float newDuration, System.Collections.Generic.Queue<UnityEngine.Vector2> newCollisionsPosition) {
            var component = CreateComponent<ShieldCollisionComponent>(ComponentIds.ShieldCollision);
            component.time = newTime;
            component.duration = newDuration;
            component.collisionsPosition = newCollisionsPosition;
            return AddComponent(ComponentIds.ShieldCollision, component);
        }

        public Entity ReplaceShieldCollision(float newTime, float newDuration, System.Collections.Generic.Queue<UnityEngine.Vector2> newCollisionsPosition) {
            var component = CreateComponent<ShieldCollisionComponent>(ComponentIds.ShieldCollision);
            component.time = newTime;
            component.duration = newDuration;
            component.collisionsPosition = newCollisionsPosition;
            ReplaceComponent(ComponentIds.ShieldCollision, component);
            return this;
        }

        public Entity RemoveShieldCollision() {
            return RemoveComponent(ComponentIds.ShieldCollision);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShieldCollision;

        public static IMatcher ShieldCollision {
            get {
                if (_matcherShieldCollision == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ShieldCollision);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherShieldCollision = matcher;
                }

                return _matcherShieldCollision;
            }
        }
    }
}
