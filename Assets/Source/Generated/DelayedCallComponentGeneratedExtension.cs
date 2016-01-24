using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DelayedCallComponent delayedCall { get { return (DelayedCallComponent)GetComponent(ComponentIds.DelayedCall); } }

        public bool hasDelayedCall { get { return HasComponent(ComponentIds.DelayedCall); } }

        static readonly Stack<DelayedCallComponent> _delayedCallComponentPool = new Stack<DelayedCallComponent>();

        public static void ClearDelayedCallComponentPool() {
            _delayedCallComponentPool.Clear();
        }

        public Entity AddDelayedCall(float newDuration, System.Action<Entitas.Entity> newOnComplete) {
            var component = _delayedCallComponentPool.Count > 0 ? _delayedCallComponentPool.Pop() : new DelayedCallComponent();
            component.duration = newDuration;
            component.onComplete = newOnComplete;
            return AddComponent(ComponentIds.DelayedCall, component);
        }

        public Entity ReplaceDelayedCall(float newDuration, System.Action<Entitas.Entity> newOnComplete) {
            var previousComponent = hasDelayedCall ? delayedCall : null;
            var component = _delayedCallComponentPool.Count > 0 ? _delayedCallComponentPool.Pop() : new DelayedCallComponent();
            component.duration = newDuration;
            component.onComplete = newOnComplete;
            ReplaceComponent(ComponentIds.DelayedCall, component);
            if (previousComponent != null) {
                _delayedCallComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDelayedCall() {
            var component = delayedCall;
            RemoveComponent(ComponentIds.DelayedCall);
            _delayedCallComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDelayedCall;

        public static IMatcher DelayedCall {
            get {
                if (_matcherDelayedCall == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.DelayedCall);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDelayedCall = matcher;
                }

                return _matcherDelayedCall;
            }
        }
    }
}
