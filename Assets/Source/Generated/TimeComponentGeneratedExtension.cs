using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public TimeComponent time { get { return (TimeComponent)GetComponent(ComponentIds.Time); } }

        public bool hasTime { get { return HasComponent(ComponentIds.Time); } }

        static readonly Stack<TimeComponent> _timeComponentPool = new Stack<TimeComponent>();

        public static void ClearTimeComponentPool() {
            _timeComponentPool.Clear();
        }

        public Entity AddTime(float newDeltaTime, float newTime, float newModificator, bool newIsPaused) {
            var component = _timeComponentPool.Count > 0 ? _timeComponentPool.Pop() : new TimeComponent();
            component.deltaTime = newDeltaTime;
            component.time = newTime;
            component.modificator = newModificator;
            component.isPaused = newIsPaused;
            return AddComponent(ComponentIds.Time, component);
        }

        public Entity ReplaceTime(float newDeltaTime, float newTime, float newModificator, bool newIsPaused) {
            var previousComponent = hasTime ? time : null;
            var component = _timeComponentPool.Count > 0 ? _timeComponentPool.Pop() : new TimeComponent();
            component.deltaTime = newDeltaTime;
            component.time = newTime;
            component.modificator = newModificator;
            component.isPaused = newIsPaused;
            ReplaceComponent(ComponentIds.Time, component);
            if (previousComponent != null) {
                _timeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTime() {
            var component = time;
            RemoveComponent(ComponentIds.Time);
            _timeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherTime;

        public static AllOfMatcher Time {
            get {
                if (_matcherTime == null) {
                    _matcherTime = new Matcher(ComponentIds.Time);
                }

                return _matcherTime;
            }
        }
    }
}
