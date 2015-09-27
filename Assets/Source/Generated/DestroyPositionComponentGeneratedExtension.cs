namespace Entitas {
    public partial class Entity {
        static readonly DestroyPositionComponent destroyPositionComponent = new DestroyPositionComponent();

        public bool isDestroyPosition {
            get { return HasComponent(ComponentIds.DestroyPosition); }
            set {
                if (value != isDestroyPosition) {
                    if (value) {
                        AddComponent(ComponentIds.DestroyPosition, destroyPositionComponent);
                    } else {
                        RemoveComponent(ComponentIds.DestroyPosition);
                    }
                }
            }
        }

        public Entity IsDestroyPosition(bool value) {
            isDestroyPosition = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherDestroyPosition;

        public static AllOfMatcher DestroyPosition {
            get {
                if (_matcherDestroyPosition == null) {
                    _matcherDestroyPosition = new Matcher(ComponentIds.DestroyPosition);
                }

                return _matcherDestroyPosition;
            }
        }
    }
}
