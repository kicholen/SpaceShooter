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
        public FirstBossComponent firstBoss { get { return (FirstBossComponent)GetComponent(ComponentIds.FirstBoss); } }

        public bool hasFirstBoss { get { return HasComponent(ComponentIds.FirstBoss); } }

        public Entity AddFirstBoss(float newRandom, float newAge, float newLaserAngle) {
            var component = CreateComponent<FirstBossComponent>(ComponentIds.FirstBoss);
            component.random = newRandom;
            component.age = newAge;
            component.laserAngle = newLaserAngle;
            return AddComponent(ComponentIds.FirstBoss, component);
        }

        public Entity ReplaceFirstBoss(float newRandom, float newAge, float newLaserAngle) {
            var component = CreateComponent<FirstBossComponent>(ComponentIds.FirstBoss);
            component.random = newRandom;
            component.age = newAge;
            component.laserAngle = newLaserAngle;
            ReplaceComponent(ComponentIds.FirstBoss, component);
            return this;
        }

        public Entity RemoveFirstBoss() {
            return RemoveComponent(ComponentIds.FirstBoss);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherFirstBoss;

        public static IMatcher FirstBoss {
            get {
                if (_matcherFirstBoss == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.FirstBoss);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherFirstBoss = matcher;
                }

                return _matcherFirstBoss;
            }
        }
    }
}
