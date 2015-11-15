using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public EventServiceComponent eventService { get { return (EventServiceComponent)GetComponent(ComponentIds.EventService); } }

        public bool hasEventService { get { return HasComponent(ComponentIds.EventService); } }

        static readonly Stack<EventServiceComponent> _eventServiceComponentPool = new Stack<EventServiceComponent>();

        public static void ClearEventServiceComponentPool() {
            _eventServiceComponentPool.Clear();
        }

        public Entity AddEventService(EventService newDispatcher) {
            var component = _eventServiceComponentPool.Count > 0 ? _eventServiceComponentPool.Pop() : new EventServiceComponent();
            component.dispatcher = newDispatcher;
            return AddComponent(ComponentIds.EventService, component);
        }

        public Entity ReplaceEventService(EventService newDispatcher) {
            var previousComponent = hasEventService ? eventService : null;
            var component = _eventServiceComponentPool.Count > 0 ? _eventServiceComponentPool.Pop() : new EventServiceComponent();
            component.dispatcher = newDispatcher;
            ReplaceComponent(ComponentIds.EventService, component);
            if (previousComponent != null) {
                _eventServiceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveEventService() {
            var component = eventService;
            RemoveComponent(ComponentIds.EventService);
            _eventServiceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherEventService;

        public static IMatcher EventService {
            get {
                if (_matcherEventService == null) {
                    _matcherEventService = Matcher.AllOf(ComponentIds.EventService);
                }

                return _matcherEventService;
            }
        }
    }
}
