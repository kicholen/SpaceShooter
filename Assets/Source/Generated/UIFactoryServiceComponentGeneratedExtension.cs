using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public UIFactoryServiceComponent uIFactoryService { get { return (UIFactoryServiceComponent)GetComponent(ComponentIds.UIFactoryService); } }

        public bool hasUIFactoryService { get { return HasComponent(ComponentIds.UIFactoryService); } }

        static readonly Stack<UIFactoryServiceComponent> _uIFactoryServiceComponentPool = new Stack<UIFactoryServiceComponent>();

        public static void ClearUIFactoryServiceComponentPool() {
            _uIFactoryServiceComponentPool.Clear();
        }

        public Entity AddUIFactoryService(IUIFactoryService newService) {
            var component = _uIFactoryServiceComponentPool.Count > 0 ? _uIFactoryServiceComponentPool.Pop() : new UIFactoryServiceComponent();
            component.service = newService;
            return AddComponent(ComponentIds.UIFactoryService, component);
        }

        public Entity ReplaceUIFactoryService(IUIFactoryService newService) {
            var previousComponent = hasUIFactoryService ? uIFactoryService : null;
            var component = _uIFactoryServiceComponentPool.Count > 0 ? _uIFactoryServiceComponentPool.Pop() : new UIFactoryServiceComponent();
            component.service = newService;
            ReplaceComponent(ComponentIds.UIFactoryService, component);
            if (previousComponent != null) {
                _uIFactoryServiceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveUIFactoryService() {
            var component = uIFactoryService;
            RemoveComponent(ComponentIds.UIFactoryService);
            _uIFactoryServiceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherUIFactoryService;

        public static IMatcher UIFactoryService {
            get {
                if (_matcherUIFactoryService == null) {
                    _matcherUIFactoryService = Matcher.AllOf(ComponentIds.UIFactoryService);
                }

                return _matcherUIFactoryService;
            }
        }
    }
}
