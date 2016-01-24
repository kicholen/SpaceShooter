using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CreateLevelComponent createLevel { get { return (CreateLevelComponent)GetComponent(ComponentIds.CreateLevel); } }

        public bool hasCreateLevel { get { return HasComponent(ComponentIds.CreateLevel); } }

        static readonly Stack<CreateLevelComponent> _createLevelComponentPool = new Stack<CreateLevelComponent>();

        public static void ClearCreateLevelComponentPool() {
            _createLevelComponentPool.Clear();
        }

        public Entity AddCreateLevel(int newLevel, string newPath) {
            var component = _createLevelComponentPool.Count > 0 ? _createLevelComponentPool.Pop() : new CreateLevelComponent();
            component.level = newLevel;
            component.path = newPath;
            return AddComponent(ComponentIds.CreateLevel, component);
        }

        public Entity ReplaceCreateLevel(int newLevel, string newPath) {
            var previousComponent = hasCreateLevel ? createLevel : null;
            var component = _createLevelComponentPool.Count > 0 ? _createLevelComponentPool.Pop() : new CreateLevelComponent();
            component.level = newLevel;
            component.path = newPath;
            ReplaceComponent(ComponentIds.CreateLevel, component);
            if (previousComponent != null) {
                _createLevelComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCreateLevel() {
            var component = createLevel;
            RemoveComponent(ComponentIds.CreateLevel);
            _createLevelComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCreateLevel;

        public static IMatcher CreateLevel {
            get {
                if (_matcherCreateLevel == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CreateLevel);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCreateLevel = matcher;
                }

                return _matcherCreateLevel;
            }
        }
    }
}
