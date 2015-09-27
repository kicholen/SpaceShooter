namespace Entitas {
    public partial class Entity {
        static readonly WeaponComponent weaponComponent = new WeaponComponent();

        public bool isWeapon {
            get { return HasComponent(ComponentIds.Weapon); }
            set {
                if (value != isWeapon) {
                    if (value) {
                        AddComponent(ComponentIds.Weapon, weaponComponent);
                    } else {
                        RemoveComponent(ComponentIds.Weapon);
                    }
                }
            }
        }

        public Entity IsWeapon(bool value) {
            isWeapon = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherWeapon;

        public static AllOfMatcher Weapon {
            get {
                if (_matcherWeapon == null) {
                    _matcherWeapon = new Matcher(ComponentIds.Weapon);
                }

                return _matcherWeapon;
            }
        }
    }
}
