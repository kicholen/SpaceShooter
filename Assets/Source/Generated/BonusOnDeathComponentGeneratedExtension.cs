using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public BonusOnDeathComponent bonusOnDeath { get { return (BonusOnDeathComponent)GetComponent(ComponentIds.BonusOnDeath); } }

        public bool hasBonusOnDeath { get { return HasComponent(ComponentIds.BonusOnDeath); } }

        static readonly Stack<BonusOnDeathComponent> _bonusOnDeathComponentPool = new Stack<BonusOnDeathComponent>();

        public static void ClearBonusOnDeathComponentPool() {
            _bonusOnDeathComponentPool.Clear();
        }

        public Entity AddBonusOnDeath(int newType) {
            var component = _bonusOnDeathComponentPool.Count > 0 ? _bonusOnDeathComponentPool.Pop() : new BonusOnDeathComponent();
            component.type = newType;
            return AddComponent(ComponentIds.BonusOnDeath, component);
        }

        public Entity ReplaceBonusOnDeath(int newType) {
            var previousComponent = hasBonusOnDeath ? bonusOnDeath : null;
            var component = _bonusOnDeathComponentPool.Count > 0 ? _bonusOnDeathComponentPool.Pop() : new BonusOnDeathComponent();
            component.type = newType;
            ReplaceComponent(ComponentIds.BonusOnDeath, component);
            if (previousComponent != null) {
                _bonusOnDeathComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveBonusOnDeath() {
            var component = bonusOnDeath;
            RemoveComponent(ComponentIds.BonusOnDeath);
            _bonusOnDeathComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherBonusOnDeath;

        public static AllOfMatcher BonusOnDeath {
            get {
                if (_matcherBonusOnDeath == null) {
                    _matcherBonusOnDeath = new Matcher(ComponentIds.BonusOnDeath);
                }

                return _matcherBonusOnDeath;
            }
        }
    }
}
