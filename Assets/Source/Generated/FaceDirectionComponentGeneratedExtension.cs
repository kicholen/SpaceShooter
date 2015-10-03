namespace Entitas {
    public partial class Entity {
        static readonly FaceDirectionComponent faceDirectionComponent = new FaceDirectionComponent();

        public bool isFaceDirection {
            get { return HasComponent(ComponentIds.FaceDirection); }
            set {
                if (value != isFaceDirection) {
                    if (value) {
                        AddComponent(ComponentIds.FaceDirection, faceDirectionComponent);
                    } else {
                        RemoveComponent(ComponentIds.FaceDirection);
                    }
                }
            }
        }

        public Entity IsFaceDirection(bool value) {
            isFaceDirection = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherFaceDirection;

        public static AllOfMatcher FaceDirection {
            get {
                if (_matcherFaceDirection == null) {
                    _matcherFaceDirection = new Matcher(ComponentIds.FaceDirection);
                }

                return _matcherFaceDirection;
            }
        }
    }
}
