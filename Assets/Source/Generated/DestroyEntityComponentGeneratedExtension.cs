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
        static IMatcher _matcherDestroyEntity;

        public static IMatcher DestroyEntity {
            get {
                if (_matcherDestroyEntity == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.DestroyEntity);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDestroyEntity = matcher;
                }

                return _matcherDestroyEntity;
            }
        }
    }
}
