using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CallOnFrameEndComponent callOnFrameEnd { get { return (CallOnFrameEndComponent)GetComponent(ComponentIds.CallOnFrameEnd); } }

        public bool hasCallOnFrameEnd { get { return HasComponent(ComponentIds.CallOnFrameEnd); } }

        static readonly Stack<CallOnFrameEndComponent> _callOnFrameEndComponentPool = new Stack<CallOnFrameEndComponent>();

        public static void ClearCallOnFrameEndComponentPool() {
            _callOnFrameEndComponentPool.Clear();
        }

        public Entity AddCallOnFrameEnd(System.Action<Entitas.Entity> newCallback) {
            var component = _callOnFrameEndComponentPool.Count > 0 ? _callOnFrameEndComponentPool.Pop() : new CallOnFrameEndComponent();
            component.callback = newCallback;
            return AddComponent(ComponentIds.CallOnFrameEnd, component);
        }

        public Entity ReplaceCallOnFrameEnd(System.Action<Entitas.Entity> newCallback) {
            var previousComponent = hasCallOnFrameEnd ? callOnFrameEnd : null;
            var component = _callOnFrameEndComponentPool.Count > 0 ? _callOnFrameEndComponentPool.Pop() : new CallOnFrameEndComponent();
            component.callback = newCallback;
            ReplaceComponent(ComponentIds.CallOnFrameEnd, component);
            if (previousComponent != null) {
                _callOnFrameEndComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCallOnFrameEnd() {
            var component = callOnFrameEnd;
            RemoveComponent(ComponentIds.CallOnFrameEnd);
            _callOnFrameEndComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCallOnFrameEnd;

        public static IMatcher CallOnFrameEnd {
            get {
                if (_matcherCallOnFrameEnd == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CallOnFrameEnd);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCallOnFrameEnd = matcher;
                }

                return _matcherCallOnFrameEnd;
            }
        }
    }
}
