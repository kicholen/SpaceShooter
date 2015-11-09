namespace Entitas {
    public partial class Entity {
        static readonly MoveWithCamera moveWithCameraComponent = new MoveWithCamera();

        public bool isMoveWithCamera {
            get { return HasComponent(ComponentIds.MoveWithCamera); }
            set {
                if (value != isMoveWithCamera) {
                    if (value) {
                        AddComponent(ComponentIds.MoveWithCamera, moveWithCameraComponent);
                    } else {
                        RemoveComponent(ComponentIds.MoveWithCamera);
                    }
                }
            }
        }

        public Entity IsMoveWithCamera(bool value) {
            isMoveWithCamera = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMoveWithCamera;

        public static IMatcher MoveWithCamera {
            get {
                if (_matcherMoveWithCamera == null) {
                    _matcherMoveWithCamera = Matcher.AllOf(ComponentIds.MoveWithCamera);
                }

                return _matcherMoveWithCamera;
            }
        }
    }
}
