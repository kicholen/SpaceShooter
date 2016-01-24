using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public UIResourceComponent uIResource { get { return (UIResourceComponent)GetComponent(ComponentIds.UIResource); } }

        public bool hasUIResource { get { return HasComponent(ComponentIds.UIResource); } }

        static readonly Stack<UIResourceComponent> _uIResourceComponentPool = new Stack<UIResourceComponent>();

        public static void ClearUIResourceComponentPool() {
            _uIResourceComponentPool.Clear();
        }

        public Entity AddUIResource(string newName) {
            var component = _uIResourceComponentPool.Count > 0 ? _uIResourceComponentPool.Pop() : new UIResourceComponent();
            component.name = newName;
            return AddComponent(ComponentIds.UIResource, component);
        }

        public Entity ReplaceUIResource(string newName) {
            var previousComponent = hasUIResource ? uIResource : null;
            var component = _uIResourceComponentPool.Count > 0 ? _uIResourceComponentPool.Pop() : new UIResourceComponent();
            component.name = newName;
            ReplaceComponent(ComponentIds.UIResource, component);
            if (previousComponent != null) {
                _uIResourceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveUIResource() {
            var component = uIResource;
            RemoveComponent(ComponentIds.UIResource);
            _uIResourceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherUIResource;

        public static IMatcher UIResource {
            get {
                if (_matcherUIResource == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.UIResource);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherUIResource = matcher;
                }

                return _matcherUIResource;
            }
        }
    }
}
