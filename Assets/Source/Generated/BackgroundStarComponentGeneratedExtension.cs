namespace Entitas {
    public partial class Entity {
        static readonly BackgroundStarComponent backgroundStarComponent = new BackgroundStarComponent();

        public bool isBackgroundStar {
            get { return HasComponent(ComponentIds.BackgroundStar); }
            set {
                if (value != isBackgroundStar) {
                    if (value) {
                        AddComponent(ComponentIds.BackgroundStar, backgroundStarComponent);
                    } else {
                        RemoveComponent(ComponentIds.BackgroundStar);
                    }
                }
            }
        }

        public Entity IsBackgroundStar(bool value) {
            isBackgroundStar = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBackgroundStar;

        public static IMatcher BackgroundStar {
            get {
                if (_matcherBackgroundStar == null) {
                    _matcherBackgroundStar = Matcher.AllOf(ComponentIds.BackgroundStar);
                }

                return _matcherBackgroundStar;
            }
        }
    }
}
