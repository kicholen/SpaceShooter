using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public StartGameComponent startGame { get { return (StartGameComponent)GetComponent(ComponentIds.StartGame); } }

        public bool hasStartGame { get { return HasComponent(ComponentIds.StartGame); } }

        static readonly Stack<StartGameComponent> _startGameComponentPool = new Stack<StartGameComponent>();

        public static void ClearStartGameComponentPool() {
            _startGameComponentPool.Clear();
        }

        public Entity AddStartGame(int newLevel) {
            var component = _startGameComponentPool.Count > 0 ? _startGameComponentPool.Pop() : new StartGameComponent();
            component.level = newLevel;
            return AddComponent(ComponentIds.StartGame, component);
        }

        public Entity ReplaceStartGame(int newLevel) {
            var previousComponent = hasStartGame ? startGame : null;
            var component = _startGameComponentPool.Count > 0 ? _startGameComponentPool.Pop() : new StartGameComponent();
            component.level = newLevel;
            ReplaceComponent(ComponentIds.StartGame, component);
            if (previousComponent != null) {
                _startGameComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveStartGame() {
            var component = startGame;
            RemoveComponent(ComponentIds.StartGame);
            _startGameComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherStartGame;

        public static IMatcher StartGame {
            get {
                if (_matcherStartGame == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.StartGame);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherStartGame = matcher;
                }

                return _matcherStartGame;
            }
        }
    }
}
