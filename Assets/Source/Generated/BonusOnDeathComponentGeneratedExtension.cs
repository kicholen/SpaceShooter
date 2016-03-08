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
        public BonusOnDeathComponent bonusOnDeath { get { return (BonusOnDeathComponent)GetComponent(ComponentIds.BonusOnDeath); } }

        public bool hasBonusOnDeath { get { return HasComponent(ComponentIds.BonusOnDeath); } }

        public Entity AddBonusOnDeath(int newType) {
            var component = CreateComponent<BonusOnDeathComponent>(ComponentIds.BonusOnDeath);
            component.type = newType;
            return AddComponent(ComponentIds.BonusOnDeath, component);
        }

        public Entity ReplaceBonusOnDeath(int newType) {
            var component = CreateComponent<BonusOnDeathComponent>(ComponentIds.BonusOnDeath);
            component.type = newType;
            ReplaceComponent(ComponentIds.BonusOnDeath, component);
            return this;
        }

        public Entity RemoveBonusOnDeath() {
            return RemoveComponent(ComponentIds.BonusOnDeath);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBonusOnDeath;

        public static IMatcher BonusOnDeath {
            get {
                if (_matcherBonusOnDeath == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.BonusOnDeath);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBonusOnDeath = matcher;
                }

                return _matcherBonusOnDeath;
            }
        }
    }
}
