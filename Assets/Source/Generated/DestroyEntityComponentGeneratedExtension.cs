namespace Entitas {
    public partial class Entity {
        static readonly DestroyEntityComponent destroyEntityComponent = new DestroyEntityComponent();

        public bool isDestroyEntity {
            get { return HasComponent(ComponentIds.DestroyEntity); }
            set {
                if (value != isDestroyEntity) {
                    if (value) {
                        AddComponent(ComponentIds.DestroyEntity, destroyEntityComponent);
                    } else {
                        RemoveComponent(ComponentIds.DestroyEntity);
                    }
                }
            }
        }

        public Entity IsDestroyEntity(bool value) {
            isDestroyEntity = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherDestroyEntity;

        public static AllOfMatcher DestroyEntity {
            get {
                if (_matcherDestroyEntity == null) {
                    _matcherDestroyEntity = new Matcher(ComponentIds.DestroyEntity);
                }

                return _matcherDestroyEntity;
            }
        }
    }
}
