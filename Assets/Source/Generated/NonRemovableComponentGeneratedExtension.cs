namespace Entitas {
    public partial class Entity {
        static readonly NonRemovableComponent nonRemovableComponent = new NonRemovableComponent();

        public bool isNonRemovable {
            get { return HasComponent(ComponentIds.NonRemovable); }
            set {
                if (value != isNonRemovable) {
                    if (value) {
                        AddComponent(ComponentIds.NonRemovable, nonRemovableComponent);
                    } else {
                        RemoveComponent(ComponentIds.NonRemovable);
                    }
                }
            }
        }

        public Entity IsNonRemovable(bool value) {
            isNonRemovable = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherNonRemovable;

        public static IMatcher NonRemovable {
            get {
                if (_matcherNonRemovable == null) {
                    _matcherNonRemovable = Matcher.AllOf(ComponentIds.NonRemovable);
                }

                return _matcherNonRemovable;
            }
        }
    }
}
