using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public BonusModelComponent bonusModel { get { return (BonusModelComponent)GetComponent(ComponentIds.BonusModel); } }

        public bool hasBonusModel { get { return HasComponent(ComponentIds.BonusModel); } }

        static readonly Stack<BonusModelComponent> _bonusModelComponentPool = new Stack<BonusModelComponent>();

        public static void ClearBonusModelComponentPool() {
            _bonusModelComponentPool.Clear();
        }

        public Entity AddBonusModel(int newType, int newMinAmount, int newMaxAmount, float newProbability, string newResource) {
            var component = _bonusModelComponentPool.Count > 0 ? _bonusModelComponentPool.Pop() : new BonusModelComponent();
            component.type = newType;
            component.minAmount = newMinAmount;
            component.maxAmount = newMaxAmount;
            component.probability = newProbability;
            component.resource = newResource;
            return AddComponent(ComponentIds.BonusModel, component);
        }

        public Entity ReplaceBonusModel(int newType, int newMinAmount, int newMaxAmount, float newProbability, string newResource) {
            var previousComponent = hasBonusModel ? bonusModel : null;
            var component = _bonusModelComponentPool.Count > 0 ? _bonusModelComponentPool.Pop() : new BonusModelComponent();
            component.type = newType;
            component.minAmount = newMinAmount;
            component.maxAmount = newMaxAmount;
            component.probability = newProbability;
            component.resource = newResource;
            ReplaceComponent(ComponentIds.BonusModel, component);
            if (previousComponent != null) {
                _bonusModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveBonusModel() {
            var component = bonusModel;
            RemoveComponent(ComponentIds.BonusModel);
            _bonusModelComponentPool.Push(component);
            return this;
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
