using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ParentComponent parent { get { return (ParentComponent)GetComponent(ComponentIds.Parent); } }

        public bool hasParent { get { return HasComponent(ComponentIds.Parent); } }

        static readonly Stack<ParentComponent> _parentComponentPool = new Stack<ParentComponent>();

        public static void ClearParentComponentPool() {
            _parentComponentPool.Clear();
        }

        public Entity AddParent(System.Collections.Generic.List<Entitas.Entity> newChildren) {
            var component = _parentComponentPool.Count > 0 ? _parentComponentPool.Pop() : new ParentComponent();
            component.children = newChildren;
            return AddComponent(ComponentIds.Parent, component);
        }

        public Entity ReplaceParent(System.Collections.Generic.List<Entitas.Entity> newChildren) {
            var previousComponent = hasParent ? parent : null;
            var component = _parentComponentPool.Count > 0 ? _parentComponentPool.Pop() : new ParentComponent();
            component.children = newChildren;
            ReplaceComponent(ComponentIds.Parent, component);
            if (previousComponent != null) {
                _parentComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveParent() {
            var component = parent;
            RemoveComponent(ComponentIds.Parent);
            _parentComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherParent;

        public static AllOfMatcher Parent {
            get {
                if (_matcherParent == null) {
                    _matcherParent = new Matcher(ComponentIds.Parent);
                }

                return _matcherParent;
            }
        }
    }
}
