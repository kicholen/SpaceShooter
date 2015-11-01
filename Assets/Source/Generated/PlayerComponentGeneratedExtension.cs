using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PlayerComponent player { get { return (PlayerComponent)GetComponent(ComponentIds.Player); } }

        public bool hasPlayer { get { return HasComponent(ComponentIds.Player); } }

        static readonly Stack<PlayerComponent> _playerComponentPool = new Stack<PlayerComponent>();

        public static void ClearPlayerComponentPool() {
            _playerComponentPool.Clear();
        }

        public Entity AddPlayer(string newName) {
            var component = _playerComponentPool.Count > 0 ? _playerComponentPool.Pop() : new PlayerComponent();
            component.name = newName;
            return AddComponent(ComponentIds.Player, component);
        }

        public Entity ReplacePlayer(string newName) {
            var previousComponent = hasPlayer ? player : null;
            var component = _playerComponentPool.Count > 0 ? _playerComponentPool.Pop() : new PlayerComponent();
            component.name = newName;
            ReplaceComponent(ComponentIds.Player, component);
            if (previousComponent != null) {
                _playerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePlayer() {
            var component = player;
            RemoveComponent(ComponentIds.Player);
            _playerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayer;

        public static IMatcher Player {
            get {
                if (_matcherPlayer == null) {
                    _matcherPlayer = Matcher.AllOf(ComponentIds.Player);
                }

                return _matcherPlayer;
            }
        }
    }
}
