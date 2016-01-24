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
        static IMatcher _matcherFaceDirection;

        public static IMatcher FaceDirection {
            get {
                if (_matcherFaceDirection == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.FaceDirection);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherFaceDirection = matcher;
                }

                return _matcherFaceDirection;
            }
        }
    }
}
