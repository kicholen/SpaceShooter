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
        public SpeedBonusComponent speedBonus { get { return (SpeedBonusComponent)GetComponent(ComponentIds.SpeedBonus); } }

        public bool hasSpeedBonus { get { return HasComponent(ComponentIds.SpeedBonus); } }

        public Entity AddSpeedBonus(float newVelocity, float newSavedVelocity, float newTime) {
            var component = CreateComponent<SpeedBonusComponent>(ComponentIds.SpeedBonus);
            component.velocity = newVelocity;
            component.savedVelocity = newSavedVelocity;
            component.time = newTime;
            return AddComponent(ComponentIds.SpeedBonus, component);
        }

        public Entity ReplaceSpeedBonus(float newVelocity, float newSavedVelocity, float newTime) {
            var component = CreateComponent<SpeedBonusComponent>(ComponentIds.SpeedBonus);
            component.velocity = newVelocity;
            component.savedVelocity = newSavedVelocity;
            component.time = newTime;
            ReplaceComponent(ComponentIds.SpeedBonus, component);
            return this;
        }

        public Entity RemoveSpeedBonus() {
            return RemoveComponent(ComponentIds.SpeedBonus);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSpeedBonus;

        public static IMatcher SpeedBonus {
            get {
                if (_matcherSpeedBonus == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SpeedBonus);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSpeedBonus = matcher;
                }

                return _matcherSpeedBonus;
            }
        }
    }
}
