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
        static AllOfMatcher _matcherCreateLevel;

        public static AllOfMatcher CreateLevel {
            get {
                if (_matcherCreateLevel == null) {
                    _matcherCreateLevel = new Matcher(ComponentIds.CreateLevel);
                }

                return _matcherCreateLevel;
            }
        }
    }
}
