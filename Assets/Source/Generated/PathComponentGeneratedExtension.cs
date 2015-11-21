using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PathComponent path { get { return (PathComponent)GetComponent(ComponentIds.Path); } }

        public bool hasPath { get { return HasComponent(ComponentIds.Path); } }

        static readonly Stack<PathComponent> _pathComponentPool = new Stack<PathComponent>();

        public static void ClearPathComponentPool() {
            _pathComponentPool.Clear();
        }

        public Entity AddPath(int newNode, float newStartY, float newDuration, PathModelComponent newPath) {
            var component = _pathComponentPool.Count > 0 ? _pathComponentPool.Pop() : new PathComponent();
            component.node = newNode;
            component.startY = newStartY;
            component.duration = newDuration;
            component.path = newPath;
            return AddComponent(ComponentIds.Path, component);
        }

        public Entity ReplacePath(int newNode, float newStartY, float newDuration, PathModelComponent newPath) {
            var previousComponent = hasPath ? path : null;
            var component = _pathComponentPool.Count > 0 ? _pathComponentPool.Pop() : new PathComponent();
            component.node = newNode;
            component.startY = newStartY;
            component.duration = newDuration;
            component.path = newPath;
            ReplaceComponent(ComponentIds.Path, component);
            if (previousComponent != null) {
                _pathComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePath() {
            var component = path;
            RemoveComponent(ComponentIds.Path);
            _pathComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPath;

        public static IMatcher Path {
            get {
                if (_matcherPath == null) {
                    _matcherPath = Matcher.AllOf(ComponentIds.Path);
                }

                return _matcherPath;
            }
        }
    }
}
