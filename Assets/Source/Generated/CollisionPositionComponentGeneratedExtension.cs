namespace Entitas {
    public partial class Entity {
        static readonly CollisionPositionComponent collisionPositionComponent = new CollisionPositionComponent();

        public bool isCollisionPosition {
            get { return HasComponent(ComponentIds.CollisionPosition); }
            set {
                if (value != isCollisionPosition) {
                    if (value) {
                        AddComponent(ComponentIds.CollisionPosition, collisionPositionComponent);
                    } else {
                        RemoveComponent(ComponentIds.CollisionPosition);
                    }
                }
            }
        }

        public Entity IsCollisionPosition(bool value) {
            isCollisionPosition = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCollisionPosition;

        public static IMatcher CollisionPosition {
            get {
                if (_matcherCollisionPosition == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CollisionPosition);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCollisionPosition = matcher;
                }

                return _matcherCollisionPosition;
            }
        }
    }
}
