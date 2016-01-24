namespace Entitas {
    public partial class Entity {
        static readonly CreatePlayerComponent createPlayerComponent = new CreatePlayerComponent();

        public bool isCreatePlayer {
            get { return HasComponent(ComponentIds.CreatePlayer); }
            set {
                if (value != isCreatePlayer) {
                    if (value) {
                        AddComponent(ComponentIds.CreatePlayer, createPlayerComponent);
                    } else {
                        RemoveComponent(ComponentIds.CreatePlayer);
                    }
                }
            }
        }

        public Entity IsCreatePlayer(bool value) {
            isCreatePlayer = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCreatePlayer;

        public static IMatcher CreatePlayer {
            get {
                if (_matcherCreatePlayer == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CreatePlayer);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCreatePlayer = matcher;
                }

                return _matcherCreatePlayer;
            }
        }
    }
}
