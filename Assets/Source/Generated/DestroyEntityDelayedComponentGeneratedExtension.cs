using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DestroyEntityDelayedComponent destroyEntityDelayed { get { return (DestroyEntityDelayedComponent)GetComponent(ComponentIds.DestroyEntityDelayed); } }

        public bool hasDestroyEntityDelayed { get { return HasComponent(ComponentIds.DestroyEntityDelayed); } }

        static readonly Stack<DestroyEntityDelayedComponent> _destroyEntityDelayedComponentPool = new Stack<DestroyEntityDelayedComponent>();

        public static void ClearDestroyEntityDelayedComponentPool() {
            _destroyEntityDelayedComponentPool.Clear();
        }

        public Entity AddDestroyEntityDelayed(float newTime) {
            var component = _destroyEntityDelayedComponentPool.Count > 0 ? _destroyEntityDelayedComponentPool.Pop() : new DestroyEntityDelayedComponent();
            component.time = newTime;
            return AddComponent(ComponentIds.DestroyEntityDelayed, component);
        }

        public Entity ReplaceDestroyEntityDelayed(float newTime) {
            var previousComponent = hasDestroyEntityDelayed ? destroyEntityDelayed : null;
            var component = _destroyEntityDelayedComponentPool.Count > 0 ? _destroyEntityDelayedComponentPool.Pop() : new DestroyEntityDelayedComponent();
            component.time = newTime;
            ReplaceComponent(ComponentIds.DestroyEntityDelayed, component);
            if (previousComponent != null) {
                _destroyEntityDelayedComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDestroyEntityDelayed() {
            var component = destroyEntityDelayed;
            RemoveComponent(ComponentIds.DestroyEntityDelayed);
            _destroyEntityDelayedComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherDestroyEntityDelayed;

        public static AllOfMatcher DestroyEntityDelayed {
            get {
                if (_matcherDestroyEntityDelayed == null) {
                    _matcherDestroyEntityDelayed = new Matcher(ComponentIds.DestroyEntityDelayed);
                }

                return _matcherDestroyEntityDelayed;
            }
        }
    }
}
