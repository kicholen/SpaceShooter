using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ShipBonusComponent shipBonus { get { return (ShipBonusComponent)GetComponent(ComponentIds.ShipBonus); } }

        public bool hasShipBonus { get { return HasComponent(ComponentIds.ShipBonus); } }

        static readonly Stack<ShipBonusComponent> _shipBonusComponentPool = new Stack<ShipBonusComponent>();

        public static void ClearShipBonusComponentPool() {
            _shipBonusComponentPool.Clear();
        }

        public Entity AddShipBonus(float newDamageBoost, float newFireRateBoost) {
            var component = _shipBonusComponentPool.Count > 0 ? _shipBonusComponentPool.Pop() : new ShipBonusComponent();
            component.damageBoost = newDamageBoost;
            component.fireRateBoost = newFireRateBoost;
            return AddComponent(ComponentIds.ShipBonus, component);
        }

        public Entity ReplaceShipBonus(float newDamageBoost, float newFireRateBoost) {
            var previousComponent = hasShipBonus ? shipBonus : null;
            var component = _shipBonusComponentPool.Count > 0 ? _shipBonusComponentPool.Pop() : new ShipBonusComponent();
            component.damageBoost = newDamageBoost;
            component.fireRateBoost = newFireRateBoost;
            ReplaceComponent(ComponentIds.ShipBonus, component);
            if (previousComponent != null) {
                _shipBonusComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveShipBonus() {
            var component = shipBonus;
            RemoveComponent(ComponentIds.ShipBonus);
            _shipBonusComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShipBonus;

        public static IMatcher ShipBonus {
            get {
                if (_matcherShipBonus == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ShipBonus);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherShipBonus = matcher;
                }

                return _matcherShipBonus;
            }
        }
    }
}
