using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SlowGameComponent slowGame { get { return (SlowGameComponent)GetComponent(ComponentIds.SlowGame); } }

        public bool hasSlowGame { get { return HasComponent(ComponentIds.SlowGame); } }

        static readonly Stack<SlowGameComponent> _slowGameComponentPool = new Stack<SlowGameComponent>();

        public static void ClearSlowGameComponentPool() {
            _slowGameComponentPool.Clear();
        }

        public Entity AddSlowGame(float newValue) {
            var component = _slowGameComponentPool.Count > 0 ? _slowGameComponentPool.Pop() : new SlowGameComponent();
            component.value = newValue;
            return AddComponent(ComponentIds.SlowGame, component);
        }

        public Entity ReplaceSlowGame(float newValue) {
            var previousComponent = hasSlowGame ? slowGame : null;
            var component = _slowGameComponentPool.Count > 0 ? _slowGameComponentPool.Pop() : new SlowGameComponent();
            component.value = newValue;
            ReplaceComponent(ComponentIds.SlowGame, component);
            if (previousComponent != null) {
                _slowGameComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSlowGame() {
            var component = slowGame;
            RemoveComponent(ComponentIds.SlowGame);
            _slowGameComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSlowGame;

        public static IMatcher SlowGame {
            get {
                if (_matcherSlowGame == null) {
                    _matcherSlowGame = Matcher.AllOf(ComponentIds.SlowGame);
                }

                return _matcherSlowGame;
            }
        }
    }
}
