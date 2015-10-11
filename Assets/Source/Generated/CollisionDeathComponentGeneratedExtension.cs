namespace Entitas {
    public partial class Entity {
        static readonly CollisionDeathComponent collisionDeathComponent = new CollisionDeathComponent();

        public bool isCollisionDeath {
            get { return HasComponent(ComponentIds.CollisionDeath); }
            set {
                if (value != isCollisionDeath) {
                    if (value) {
                        AddComponent(ComponentIds.CollisionDeath, collisionDeathComponent);
                    } else {
                        RemoveComponent(ComponentIds.CollisionDeath);
                    }
                }
            }
        }

        public Entity IsCollisionDeath(bool value) {
            isCollisionDeath = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCollisionDeath;

        public static AllOfMatcher CollisionDeath {
            get {
                if (_matcherCollisionDeath == null) {
                    _matcherCollisionDeath = new Matcher(ComponentIds.CollisionDeath);
                }

                return _matcherCollisionDeath;
            }
        }
    }
}
