using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public BonusComponent bonus { get { return (BonusComponent)GetComponent(ComponentIds.Bonus); } }

        public bool hasBonus { get { return HasComponent(ComponentIds.Bonus); } }

        static readonly Stack<BonusComponent> _bonusComponentPool = new Stack<BonusComponent>();

        public static void ClearBonusComponentPool() {
            _bonusComponentPool.Clear();
        }

        public Entity AddBonus(int newType) {
            var component = _bonusComponentPool.Count > 0 ? _bonusComponentPool.Pop() : new BonusComponent();
            component.type = newType;
            return AddComponent(ComponentIds.Bonus, component);
        }

        public Entity ReplaceBonus(int newType) {
            var previousComponent = hasBonus ? bonus : null;
            var component = _bonusComponentPool.Count > 0 ? _bonusComponentPool.Pop() : new BonusComponent();
            component.type = newType;
            ReplaceComponent(ComponentIds.Bonus, component);
            if (previousComponent != null) {
                _bonusComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveBonus() {
            var component = bonus;
            RemoveComponent(ComponentIds.Bonus);
            _bonusComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBonus;

        public static IMatcher Bonus {
            get {
                if (_matcherBonus == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Bonus);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBonus = matcher;
                }

                return _matcherBonus;
            }
        }
    }
}
