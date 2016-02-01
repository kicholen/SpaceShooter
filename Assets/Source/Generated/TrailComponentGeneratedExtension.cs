using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public TrailComponent trail { get { return (TrailComponent)GetComponent(ComponentIds.Trail); } }

        public bool hasTrail { get { return HasComponent(ComponentIds.Trail); } }

        static readonly Stack<TrailComponent> _trailComponentPool = new Stack<TrailComponent>();

        public static void ClearTrailComponentPool() {
            _trailComponentPool.Clear();
        }

        public Entity AddTrail(float newTime) {
            var component = _trailComponentPool.Count > 0 ? _trailComponentPool.Pop() : new TrailComponent();
            component.time = newTime;
            return AddComponent(ComponentIds.Trail, component);
        }

        public Entity ReplaceTrail(float newTime) {
            var previousComponent = hasTrail ? trail : null;
            var component = _trailComponentPool.Count > 0 ? _trailComponentPool.Pop() : new TrailComponent();
            component.time = newTime;
            ReplaceComponent(ComponentIds.Trail, component);
            if (previousComponent != null) {
                _trailComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTrail() {
            var component = trail;
            RemoveComponent(ComponentIds.Trail);
            _trailComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTrail;

        public static IMatcher Trail {
            get {
                if (_matcherTrail == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Trail);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTrail = matcher;
                }

                return _matcherTrail;
            }
        }
    }
}
