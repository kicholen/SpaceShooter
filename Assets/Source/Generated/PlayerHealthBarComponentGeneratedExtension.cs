using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PlayerHealthBarComponent playerHealthBar { get { return (PlayerHealthBarComponent)GetComponent(ComponentIds.PlayerHealthBar); } }

        public bool hasPlayerHealthBar { get { return HasComponent(ComponentIds.PlayerHealthBar); } }

        static readonly Stack<PlayerHealthBarComponent> _playerHealthBarComponentPool = new Stack<PlayerHealthBarComponent>();

        public static void ClearPlayerHealthBarComponentPool() {
            _playerHealthBarComponentPool.Clear();
        }

        public Entity AddPlayerHealthBar(float newCurrentValue, float newTotalValue) {
            var component = _playerHealthBarComponentPool.Count > 0 ? _playerHealthBarComponentPool.Pop() : new PlayerHealthBarComponent();
            component.currentValue = newCurrentValue;
            component.totalValue = newTotalValue;
            return AddComponent(ComponentIds.PlayerHealthBar, component);
        }

        public Entity ReplacePlayerHealthBar(float newCurrentValue, float newTotalValue) {
            var previousComponent = hasPlayerHealthBar ? playerHealthBar : null;
            var component = _playerHealthBarComponentPool.Count > 0 ? _playerHealthBarComponentPool.Pop() : new PlayerHealthBarComponent();
            component.currentValue = newCurrentValue;
            component.totalValue = newTotalValue;
            ReplaceComponent(ComponentIds.PlayerHealthBar, component);
            if (previousComponent != null) {
                _playerHealthBarComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePlayerHealthBar() {
            var component = playerHealthBar;
            RemoveComponent(ComponentIds.PlayerHealthBar);
            _playerHealthBarComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayerHealthBar;

        public static IMatcher PlayerHealthBar {
            get {
                if (_matcherPlayerHealthBar == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PlayerHealthBar);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPlayerHealthBar = matcher;
                }

                return _matcherPlayerHealthBar;
            }
        }
    }
}
