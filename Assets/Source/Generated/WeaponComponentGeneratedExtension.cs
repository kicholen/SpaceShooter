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
        static IMatcher _matcherWeapon;

        public static IMatcher Weapon {
            get {
                if (_matcherWeapon == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Weapon);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherWeapon = matcher;
                }

                return _matcherWeapon;
            }
        }
    }
}
