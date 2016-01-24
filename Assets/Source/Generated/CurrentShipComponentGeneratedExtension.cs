using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CurrentShipComponent currentShip { get { return (CurrentShipComponent)GetComponent(ComponentIds.CurrentShip); } }

        public bool hasCurrentShip { get { return HasComponent(ComponentIds.CurrentShip); } }

        static readonly Stack<CurrentShipComponent> _currentShipComponentPool = new Stack<CurrentShipComponent>();

        public static void ClearCurrentShipComponentPool() {
            _currentShipComponentPool.Clear();
        }

        public Entity AddCurrentShip(ShipModelComponent newModel) {
            var component = _currentShipComponentPool.Count > 0 ? _currentShipComponentPool.Pop() : new CurrentShipComponent();
            component.model = newModel;
            return AddComponent(ComponentIds.CurrentShip, component);
        }

        public Entity ReplaceCurrentShip(ShipModelComponent newModel) {
            var previousComponent = hasCurrentShip ? currentShip : null;
            var component = _currentShipComponentPool.Count > 0 ? _currentShipComponentPool.Pop() : new CurrentShipComponent();
            component.model = newModel;
            ReplaceComponent(ComponentIds.CurrentShip, component);
            if (previousComponent != null) {
                _currentShipComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCurrentShip() {
            var component = currentShip;
            RemoveComponent(ComponentIds.CurrentShip);
            _currentShipComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCurrentShip;

        public static IMatcher CurrentShip {
            get {
                if (_matcherCurrentShip == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CurrentShip);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCurrentShip = matcher;
                }

                return _matcherCurrentShip;
            }
        }
    }
}
