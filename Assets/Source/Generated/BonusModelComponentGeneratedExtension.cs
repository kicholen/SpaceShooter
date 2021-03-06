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
        public BonusModelComponent bonusModel { get { return (BonusModelComponent)GetComponent(ComponentIds.BonusModel); } }

        public bool hasBonusModel { get { return HasComponent(ComponentIds.BonusModel); } }

        public Entity AddBonusModel(int newId, int newType, int newMinAmount, int newMaxAmount, float newProbability, string newResource) {
            var component = CreateComponent<BonusModelComponent>(ComponentIds.BonusModel);
            component.id = newId;
            component.type = newType;
            component.minAmount = newMinAmount;
            component.maxAmount = newMaxAmount;
            component.probability = newProbability;
            component.resource = newResource;
            return AddComponent(ComponentIds.BonusModel, component);
        }

        public Entity ReplaceBonusModel(int newId, int newType, int newMinAmount, int newMaxAmount, float newProbability, string newResource) {
            var component = CreateComponent<BonusModelComponent>(ComponentIds.BonusModel);
            component.id = newId;
            component.type = newType;
            component.minAmount = newMinAmount;
            component.maxAmount = newMaxAmount;
            component.probability = newProbability;
            component.resource = newResource;
            ReplaceComponent(ComponentIds.BonusModel, component);
            return this;
        }

        public Entity RemoveBonusModel() {
            return RemoveComponent(ComponentIds.BonusModel);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBonusModel;

        public static IMatcher BonusModel {
            get {
                if (_matcherBonusModel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.BonusModel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBonusModel = matcher;
                }

                return _matcherBonusModel;
            }
        }
    }
}
