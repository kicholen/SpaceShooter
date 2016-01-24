namespace Entitas {
    public partial class Entity {
        static readonly CreateShipComponent createShipComponent = new CreateShipComponent();

        public bool isCreateShip {
            get { return HasComponent(ComponentIds.CreateShip); }
            set {
                if (value != isCreateShip) {
                    if (value) {
                        AddComponent(ComponentIds.CreateShip, createShipComponent);
                    } else {
                        RemoveComponent(ComponentIds.CreateShip);
                    }
                }
            }
        }

        public Entity IsCreateShip(bool value) {
            isCreateShip = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCreateShip;

        public static IMatcher CreateShip {
            get {
                if (_matcherCreateShip == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CreateShip);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCreateShip = matcher;
                }

                return _matcherCreateShip;
            }
        }
    }
}
