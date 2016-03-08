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
        public CurrentShipComponent currentShip { get { return (CurrentShipComponent)GetComponent(ComponentIds.CurrentShip); } }

        public bool hasCurrentShip { get { return HasComponent(ComponentIds.CurrentShip); } }

        public Entity AddCurrentShip(ShipModelComponent newModel) {
            var component = CreateComponent<CurrentShipComponent>(ComponentIds.CurrentShip);
            component.model = newModel;
            return AddComponent(ComponentIds.CurrentShip, component);
        }

        public Entity ReplaceCurrentShip(ShipModelComponent newModel) {
            var component = CreateComponent<CurrentShipComponent>(ComponentIds.CurrentShip);
            component.model = newModel;
            ReplaceComponent(ComponentIds.CurrentShip, component);
            return this;
        }

        public Entity RemoveCurrentShip() {
            return RemoveComponent(ComponentIds.CurrentShip);
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
