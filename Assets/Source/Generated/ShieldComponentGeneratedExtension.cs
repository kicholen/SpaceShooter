namespace Entitas {
    public partial class Entity {
        static readonly ShieldComponent shieldComponent = new ShieldComponent();

        public bool isShield {
            get { return HasComponent(ComponentIds.Shield); }
            set {
                if (value != isShield) {
                    if (value) {
                        AddComponent(ComponentIds.Shield, shieldComponent);
                    } else {
                        RemoveComponent(ComponentIds.Shield);
                    }
                }
            }
        }

        public Entity IsShield(bool value) {
            isShield = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherShield;

        public static IMatcher Shield {
            get {
                if (_matcherShield == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Shield);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherShield = matcher;
                }

                return _matcherShield;
            }
        }
    }
}
