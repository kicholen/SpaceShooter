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
                    _matcherCreateShip = Matcher.AllOf(ComponentIds.CreateShip);
                }

                return _matcherCreateShip;
            }
        }
    }
}
