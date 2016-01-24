using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AlphaComponent alpha { get { return (AlphaComponent)GetComponent(ComponentIds.Alpha); } }

        public bool hasAlpha { get { return HasComponent(ComponentIds.Alpha); } }

        static readonly Stack<AlphaComponent> _alphaComponentPool = new Stack<AlphaComponent>();

        public static void ClearAlphaComponentPool() {
            _alphaComponentPool.Clear();
        }

        public Entity AddAlpha(float newTime, float newTotalTime) {
            var component = _alphaComponentPool.Count > 0 ? _alphaComponentPool.Pop() : new AlphaComponent();
            component.time = newTime;
            component.totalTime = newTotalTime;
            return AddComponent(ComponentIds.Alpha, component);
        }

        public Entity ReplaceAlpha(float newTime, float newTotalTime) {
            var previousComponent = hasAlpha ? alpha : null;
            var component = _alphaComponentPool.Count > 0 ? _alphaComponentPool.Pop() : new AlphaComponent();
            component.time = newTime;
            component.totalTime = newTotalTime;
            ReplaceComponent(ComponentIds.Alpha, component);
            if (previousComponent != null) {
                _alphaComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAlpha() {
            var component = alpha;
            RemoveComponent(ComponentIds.Alpha);
            _alphaComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherAlpha;

        public static IMatcher Alpha {
            get {
                if (_matcherAlpha == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Alpha);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherAlpha = matcher;
                }

                return _matcherAlpha;
            }
        }
    }
}
