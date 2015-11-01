using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ShipModelComponent shipModel { get { return (ShipModelComponent)GetComponent(ComponentIds.ShipModel); } }

        public bool hasShipModel { get { return HasComponent(ComponentIds.ShipModel); } }

        static readonly Stack<ShipModelComponent> _shipModelComponentPool = new Stack<ShipModelComponent>();

        public static void ClearShipModelComponentPool() {
            _shipModelComponentPool.Clear();
        }

        public Entity AddShipModel(string newAsset) {
            var component = _shipModelComponentPool.Count > 0 ? _shipModelComponentPool.Pop() : new ShipModelComponent();
            component.asset = newAsset;
            return AddComponent(ComponentIds.ShipModel, component);
        }

        public Entity ReplaceShipModel(string newAsset) {
            var previousComponent = hasShipModel ? shipModel : null;
            var component = _shipModelComponentPool.Count > 0 ? _shipModelComponentPool.Pop() : new ShipModelComponent();
            component.asset = newAsset;
            ReplaceComponent(ComponentIds.ShipModel, component);
            if (previousComponent != null) {
                _shipModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveShipModel() {
            var component = shipModel;
            RemoveComponent(ComponentIds.ShipModel);
            _shipModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShipModel;

        public static IMatcher ShipModel {
            get {
                if (_matcherShipModel == null) {
                    _matcherShipModel = Matcher.AllOf(ComponentIds.ShipModel);
                }

                return _matcherShipModel;
            }
        }
    }
}
