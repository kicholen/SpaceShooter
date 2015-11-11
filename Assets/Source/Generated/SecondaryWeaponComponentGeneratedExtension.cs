namespace Entitas {
    public partial class Entity {
        static readonly SecondaryWeaponComponent secondaryWeaponComponent = new SecondaryWeaponComponent();

        public bool isSecondaryWeapon {
            get { return HasComponent(ComponentIds.SecondaryWeapon); }
            set {
                if (value != isSecondaryWeapon) {
                    if (value) {
                        AddComponent(ComponentIds.SecondaryWeapon, secondaryWeaponComponent);
                    } else {
                        RemoveComponent(ComponentIds.SecondaryWeapon);
                    }
                }
            }
        }

        public Entity IsSecondaryWeapon(bool value) {
            isSecondaryWeapon = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSecondaryWeapon;

        public static IMatcher SecondaryWeapon {
            get {
                if (_matcherSecondaryWeapon == null) {
                    _matcherSecondaryWeapon = Matcher.AllOf(ComponentIds.SecondaryWeapon);
                }

                return _matcherSecondaryWeapon;
            }
        }
    }
}
