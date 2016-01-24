using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public TweenOnDeathComponent tweenOnDeath { get { return (TweenOnDeathComponent)GetComponent(ComponentIds.TweenOnDeath); } }

        public bool hasTweenOnDeath { get { return HasComponent(ComponentIds.TweenOnDeath); } }

        static readonly Stack<TweenOnDeathComponent> _tweenOnDeathComponentPool = new Stack<TweenOnDeathComponent>();

        public static void ClearTweenOnDeathComponentPool() {
            _tweenOnDeathComponentPool.Clear();
        }

        public Entity AddTweenOnDeath(float newDuration, float newOffset) {
            var component = _tweenOnDeathComponentPool.Count > 0 ? _tweenOnDeathComponentPool.Pop() : new TweenOnDeathComponent();
            component.duration = newDuration;
            component.offset = newOffset;
            return AddComponent(ComponentIds.TweenOnDeath, component);
        }

        public Entity ReplaceTweenOnDeath(float newDuration, float newOffset) {
            var previousComponent = hasTweenOnDeath ? tweenOnDeath : null;
            var component = _tweenOnDeathComponentPool.Count > 0 ? _tweenOnDeathComponentPool.Pop() : new TweenOnDeathComponent();
            component.duration = newDuration;
            component.offset = newOffset;
            ReplaceComponent(ComponentIds.TweenOnDeath, component);
            if (previousComponent != null) {
                _tweenOnDeathComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTweenOnDeath() {
            var component = tweenOnDeath;
            RemoveComponent(ComponentIds.TweenOnDeath);
            _tweenOnDeathComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTweenOnDeath;

        public static IMatcher TweenOnDeath {
            get {
                if (_matcherTweenOnDeath == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TweenOnDeath);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTweenOnDeath = matcher;
                }

                return _matcherTweenOnDeath;
            }
        }
    }
}
