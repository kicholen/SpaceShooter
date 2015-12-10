using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PlayerModelComponent playerModel { get { return (PlayerModelComponent)GetComponent(ComponentIds.PlayerModel); } }

        public bool hasPlayerModel { get { return HasComponent(ComponentIds.PlayerModel); } }

        static readonly Stack<PlayerModelComponent> _playerModelComponentPool = new Stack<PlayerModelComponent>();

        public static void ClearPlayerModelComponentPool() {
            _playerModelComponentPool.Clear();
        }

        public Entity AddPlayerModel(string newName) {
            var component = _playerModelComponentPool.Count > 0 ? _playerModelComponentPool.Pop() : new PlayerModelComponent();
            component.name = newName;
            return AddComponent(ComponentIds.PlayerModel, component);
        }

        public Entity ReplacePlayerModel(string newName) {
            var previousComponent = hasPlayerModel ? playerModel : null;
            var component = _playerModelComponentPool.Count > 0 ? _playerModelComponentPool.Pop() : new PlayerModelComponent();
            component.name = newName;
            ReplaceComponent(ComponentIds.PlayerModel, component);
            if (previousComponent != null) {
                _playerModelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePlayerModel() {
            var component = playerModel;
            RemoveComponent(ComponentIds.PlayerModel);
            _playerModelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayerModel;

        public static IMatcher PlayerModel {
            get {
                if (_matcherPlayerModel == null) {
                    _matcherPlayerModel = Matcher.AllOf(ComponentIds.PlayerModel);
                }

                return _matcherPlayerModel;
            }
        }
    }
}
