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
        public ExplosionOnDeathComponent explosionOnDeath { get { return (ExplosionOnDeathComponent)GetComponent(ComponentIds.ExplosionOnDeath); } }

        public bool hasExplosionOnDeath { get { return HasComponent(ComponentIds.ExplosionOnDeath); } }

        public Entity AddExplosionOnDeath(float newLifetime, string newResource) {
            var component = CreateComponent<ExplosionOnDeathComponent>(ComponentIds.ExplosionOnDeath);
            component.lifetime = newLifetime;
            component.resource = newResource;
            return AddComponent(ComponentIds.ExplosionOnDeath, component);
        }

        public Entity ReplaceExplosionOnDeath(float newLifetime, string newResource) {
            var component = CreateComponent<ExplosionOnDeathComponent>(ComponentIds.ExplosionOnDeath);
            component.lifetime = newLifetime;
            component.resource = newResource;
            ReplaceComponent(ComponentIds.ExplosionOnDeath, component);
            return this;
        }

        public Entity RemoveExplosionOnDeath() {
            return RemoveComponent(ComponentIds.ExplosionOnDeath);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherExplosionOnDeath;

        public static IMatcher ExplosionOnDeath {
            get {
                if (_matcherExplosionOnDeath == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ExplosionOnDeath);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherExplosionOnDeath = matcher;
                }

                return _matcherExplosionOnDeath;
            }
        }
    }
}
