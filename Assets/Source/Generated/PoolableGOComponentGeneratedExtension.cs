using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PoolableGOComponent poolableGO { get { return (PoolableGOComponent)GetComponent(ComponentIds.PoolableGO); } }

        public bool hasPoolableGO { get { return HasComponent(ComponentIds.PoolableGO); } }

        static readonly Stack<PoolableGOComponent> _poolableGOComponentPool = new Stack<PoolableGOComponent>();

        public static void ClearPoolableGOComponentPool() {
            _poolableGOComponentPool.Clear();
        }

        public Entity AddPoolableGO(string newName, System.Collections.Generic.Queue<UnityEngine.GameObject> newQueue) {
            var component = _poolableGOComponentPool.Count > 0 ? _poolableGOComponentPool.Pop() : new PoolableGOComponent();
            component.name = newName;
            component.queue = newQueue;
            return AddComponent(ComponentIds.PoolableGO, component);
        }

        public Entity ReplacePoolableGO(string newName, System.Collections.Generic.Queue<UnityEngine.GameObject> newQueue) {
            var previousComponent = hasPoolableGO ? poolableGO : null;
            var component = _poolableGOComponentPool.Count > 0 ? _poolableGOComponentPool.Pop() : new PoolableGOComponent();
            component.name = newName;
            component.queue = newQueue;
            ReplaceComponent(ComponentIds.PoolableGO, component);
            if (previousComponent != null) {
                _poolableGOComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePoolableGO() {
            var component = poolableGO;
            RemoveComponent(ComponentIds.PoolableGO);
            _poolableGOComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherPoolableGO;

        public static AllOfMatcher PoolableGO {
            get {
                if (_matcherPoolableGO == null) {
                    _matcherPoolableGO = new Matcher(ComponentIds.PoolableGO);
                }

                return _matcherPoolableGO;
            }
        }
    }
}
