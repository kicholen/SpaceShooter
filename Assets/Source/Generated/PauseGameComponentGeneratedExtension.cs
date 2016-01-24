namespace Entitas {
    public partial class Entity {
        static readonly PauseGameComponent pauseGameComponent = new PauseGameComponent();

        public bool isPauseGame {
            get { return HasComponent(ComponentIds.PauseGame); }
            set {
                if (value != isPauseGame) {
                    if (value) {
                        AddComponent(ComponentIds.PauseGame, pauseGameComponent);
                    } else {
                        RemoveComponent(ComponentIds.PauseGame);
                    }
                }
            }
        }

        public Entity IsPauseGame(bool value) {
            isPauseGame = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPauseGame;

        public static IMatcher PauseGame {
            get {
                if (_matcherPauseGame == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PauseGame);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPauseGame = matcher;
                }

                return _matcherPauseGame;
            }
        }
    }
}
