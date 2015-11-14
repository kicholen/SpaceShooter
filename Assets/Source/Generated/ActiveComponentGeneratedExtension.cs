namespace Entitas {
    public partial class Entity {
        static readonly ActiveComponent activeComponent = new ActiveComponent();

        public bool isActive {
            get { return HasComponent(ComponentIds.Active); }
            set {
                if (value != isActive) {
                    if (value) {
                        AddComponent(ComponentIds.Active, activeComponent);
                    } else {
                        RemoveComponent(ComponentIds.Active);
                    }
                }
            }
        }

        public Entity IsActive(bool value) {
            isActive = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherActive;

        public static IMatcher Active {
            get {
                if (_matcherActive == null) {
                    _matcherActive = Matcher.AllOf(ComponentIds.Active);
                }

                return _matcherActive;
            }
        }
    }
}