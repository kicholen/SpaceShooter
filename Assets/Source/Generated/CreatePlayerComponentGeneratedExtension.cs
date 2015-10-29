using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CreatePlayerComponent createPlayer { get { return (CreatePlayerComponent)GetComponent(ComponentIds.CreatePlayer); } }

        public bool hasCreatePlayer { get { return HasComponent(ComponentIds.CreatePlayer); } }

        static readonly Stack<CreatePlayerComponent> _createPlayerComponentPool = new Stack<CreatePlayerComponent>();

        public static void ClearCreatePlayerComponentPool() {
            _createPlayerComponentPool.Clear();
        }

        public Entity AddCreatePlayer(string newPath) {
            var component = _createPlayerComponentPool.Count > 0 ? _createPlayerComponentPool.Pop() : new CreatePlayerComponent();
            component.path = newPath;
            return AddComponent(ComponentIds.CreatePlayer, component);
        }

        public Entity ReplaceCreatePlayer(string newPath) {
            var previousComponent = hasCreatePlayer ? createPlayer : null;
            var component = _createPlayerComponentPool.Count > 0 ? _createPlayerComponentPool.Pop() : new CreatePlayerComponent();
            component.path = newPath;
            ReplaceComponent(ComponentIds.CreatePlayer, component);
            if (previousComponent != null) {
                _createPlayerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCreatePlayer() {
            var component = createPlayer;
            RemoveComponent(ComponentIds.CreatePlayer);
            _createPlayerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCreatePlayer;

        public static AllOfMatcher CreatePlayer {
            get {
                if (_matcherCreatePlayer == null) {
                    _matcherCreatePlayer = new Matcher(ComponentIds.CreatePlayer);
                }

                return _matcherCreatePlayer;
            }
        }
    }
}
