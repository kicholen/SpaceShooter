namespace Entitas {
    public partial class Entity {
        static readonly EndGameComponent endGameComponent = new EndGameComponent();

        public bool isEndGame {
            get { return HasComponent(ComponentIds.EndGame); }
            set {
                if (value != isEndGame) {
                    if (value) {
                        AddComponent(ComponentIds.EndGame, endGameComponent);
                    } else {
                        RemoveComponent(ComponentIds.EndGame);
                    }
                }
            }
        }

        public Entity IsEndGame(bool value) {
            isEndGame = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherEndGame;

        public static IMatcher EndGame {
            get {
                if (_matcherEndGame == null) {
                    _matcherEndGame = Matcher.AllOf(ComponentIds.EndGame);
                }

                return _matcherEndGame;
            }
        }
    }
}
