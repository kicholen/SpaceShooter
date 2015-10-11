namespace Entitas {
    public partial class Entity {
        static readonly RestartGameComponent restartGameComponent = new RestartGameComponent();

        public bool isRestartGame {
            get { return HasComponent(ComponentIds.RestartGame); }
            set {
                if (value != isRestartGame) {
                    if (value) {
                        AddComponent(ComponentIds.RestartGame, restartGameComponent);
                    } else {
                        RemoveComponent(ComponentIds.RestartGame);
                    }
                }
            }
        }

        public Entity IsRestartGame(bool value) {
            isRestartGame = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherRestartGame;

        public static AllOfMatcher RestartGame {
            get {
                if (_matcherRestartGame == null) {
                    _matcherRestartGame = new Matcher(ComponentIds.RestartGame);
                }

                return _matcherRestartGame;
            }
        }
    }
}
