namespace Entitas {
    public partial class Entity {
        static readonly TargetMissileComponent targetMissileComponent = new TargetMissileComponent();

        public bool isTargetMissile {
            get { return HasComponent(ComponentIds.TargetMissile); }
            set {
                if (value != isTargetMissile) {
                    if (value) {
                        AddComponent(ComponentIds.TargetMissile, targetMissileComponent);
                    } else {
                        RemoveComponent(ComponentIds.TargetMissile);
                    }
                }
            }
        }

        public Entity IsTargetMissile(bool value) {
            isTargetMissile = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTargetMissile;

        public static IMatcher TargetMissile {
            get {
                if (_matcherTargetMissile == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TargetMissile);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTargetMissile = matcher;
                }

                return _matcherTargetMissile;
            }
        }
    }
}
