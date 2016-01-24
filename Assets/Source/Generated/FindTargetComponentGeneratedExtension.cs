using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public FindTargetComponent findTarget { get { return (FindTargetComponent)GetComponent(ComponentIds.FindTarget); } }

        public bool hasFindTarget { get { return HasComponent(ComponentIds.FindTarget); } }

        static readonly Stack<FindTargetComponent> _findTargetComponentPool = new Stack<FindTargetComponent>();

        public static void ClearFindTargetComponentPool() {
            _findTargetComponentPool.Clear();
        }

        public Entity AddFindTarget(int newTargetCollisionType) {
            var component = _findTargetComponentPool.Count > 0 ? _findTargetComponentPool.Pop() : new FindTargetComponent();
            component.targetCollisionType = newTargetCollisionType;
            return AddComponent(ComponentIds.FindTarget, component);
        }

        public Entity ReplaceFindTarget(int newTargetCollisionType) {
            var previousComponent = hasFindTarget ? findTarget : null;
            var component = _findTargetComponentPool.Count > 0 ? _findTargetComponentPool.Pop() : new FindTargetComponent();
            component.targetCollisionType = newTargetCollisionType;
            ReplaceComponent(ComponentIds.FindTarget, component);
            if (previousComponent != null) {
                _findTargetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFindTarget() {
            var component = findTarget;
            RemoveComponent(ComponentIds.FindTarget);
            _findTargetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherFindTarget;

        public static IMatcher FindTarget {
            get {
                if (_matcherFindTarget == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.FindTarget);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherFindTarget = matcher;
                }

                return _matcherFindTarget;
            }
        }
    }
}
