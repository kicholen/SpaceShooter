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
                    _matcherPauseGame = Matcher.AllOf(ComponentIds.PauseGame);
                }

                return _matcherPauseGame;
            }
        }
    }
}
