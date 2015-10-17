using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public FollowTargetComponent followTarget { get { return (FollowTargetComponent)GetComponent(ComponentIds.FollowTarget); } }

        public bool hasFollowTarget { get { return HasComponent(ComponentIds.FollowTarget); } }

        static readonly Stack<FollowTargetComponent> _followTargetComponentPool = new Stack<FollowTargetComponent>();

        public static void ClearFollowTargetComponentPool() {
            _followTargetComponentPool.Clear();
        }

        public Entity AddFollowTarget(Entitas.Entity newTarget) {
            var component = _followTargetComponentPool.Count > 0 ? _followTargetComponentPool.Pop() : new FollowTargetComponent();
            component.target = newTarget;
            return AddComponent(ComponentIds.FollowTarget, component);
        }

        public Entity ReplaceFollowTarget(Entitas.Entity newTarget) {
            var previousComponent = hasFollowTarget ? followTarget : null;
            var component = _followTargetComponentPool.Count > 0 ? _followTargetComponentPool.Pop() : new FollowTargetComponent();
            component.target = newTarget;
            ReplaceComponent(ComponentIds.FollowTarget, component);
            if (previousComponent != null) {
                _followTargetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFollowTarget() {
            var component = followTarget;
            RemoveComponent(ComponentIds.FollowTarget);
            _followTargetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherFollowTarget;

        public static AllOfMatcher FollowTarget {
            get {
                if (_matcherFollowTarget == null) {
                    _matcherFollowTarget = new Matcher(ComponentIds.FollowTarget);
                }

                return _matcherFollowTarget;
            }
        }
    }
}
