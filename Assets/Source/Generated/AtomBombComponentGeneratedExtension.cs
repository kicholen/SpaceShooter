namespace Entitas {
    public partial class Entity {
        static readonly AtomBombComponent atomBombComponent = new AtomBombComponent();

        public bool isAtomBomb {
            get { return HasComponent(ComponentIds.AtomBomb); }
            set {
                if (value != isAtomBomb) {
                    if (value) {
                        AddComponent(ComponentIds.AtomBomb, atomBombComponent);
                    } else {
                        RemoveComponent(ComponentIds.AtomBomb);
                    }
                }
            }
        }

        public Entity IsAtomBomb(bool value) {
            isAtomBomb = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherAtomBomb;

        public static IMatcher AtomBomb {
            get {
                if (_matcherAtomBomb == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.AtomBomb);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherAtomBomb = matcher;
                }

                return _matcherAtomBomb;
            }
        }
    }
}