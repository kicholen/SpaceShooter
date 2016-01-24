using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MovingBlockadeComponent movingBlockade { get { return (MovingBlockadeComponent)GetComponent(ComponentIds.MovingBlockade); } }

        public bool hasMovingBlockade { get { return HasComponent(ComponentIds.MovingBlockade); } }

        static readonly Stack<MovingBlockadeComponent> _movingBlockadeComponentPool = new Stack<MovingBlockadeComponent>();

        public static void ClearMovingBlockadeComponentPool() {
            _movingBlockadeComponentPool.Clear();
        }

        public Entity AddMovingBlockade(float newOffset, float newDirection, float newTime, float newDuration, float newStopDuration) {
            var component = _movingBlockadeComponentPool.Count > 0 ? _movingBlockadeComponentPool.Pop() : new MovingBlockadeComponent();
            component.offset = newOffset;
            component.direction = newDirection;
            component.time = newTime;
            component.duration = newDuration;
            component.stopDuration = newStopDuration;
            return AddComponent(ComponentIds.MovingBlockade, component);
        }

        public Entity ReplaceMovingBlockade(float newOffset, float newDirection, float newTime, float newDuration, float newStopDuration) {
            var previousComponent = hasMovingBlockade ? movingBlockade : null;
            var component = _movingBlockadeComponentPool.Count > 0 ? _movingBlockadeComponentPool.Pop() : new MovingBlockadeComponent();
            component.offset = newOffset;
            component.direction = newDirection;
            component.time = newTime;
            component.duration = newDuration;
            component.stopDuration = newStopDuration;
            ReplaceComponent(ComponentIds.MovingBlockade, component);
            if (previousComponent != null) {
                _movingBlockadeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMovingBlockade() {
            var component = movingBlockade;
            RemoveComponent(ComponentIds.MovingBlockade);
            _movingBlockadeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMovingBlockade;

        public static IMatcher MovingBlockade {
            get {
                if (_matcherMovingBlockade == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MovingBlockade);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMovingBlockade = matcher;
                }

                return _matcherMovingBlockade;
            }
        }
    }
}
