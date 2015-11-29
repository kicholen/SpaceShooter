using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public TweenPositionComponent tweenPosition { get { return (TweenPositionComponent)GetComponent(ComponentIds.TweenPosition); } }

        public bool hasTweenPosition { get { return HasComponent(ComponentIds.TweenPosition); } }

        static readonly Stack<TweenPositionComponent> _tweenPositionComponentPool = new Stack<TweenPositionComponent>();

        public static void ClearTweenPositionComponentPool() {
            _tweenPositionComponentPool.Clear();
        }

        public Entity AddTweenPosition(float newTime, float newDuration, int newEase, UnityEngine.Vector2 newFromVector, UnityEngine.Vector2 newToVector, System.Action<Entitas.Entity> newOnComplete, System.Action<Entitas.Entity> newOnUpdate) {
            var component = _tweenPositionComponentPool.Count > 0 ? _tweenPositionComponentPool.Pop() : new TweenPositionComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.ease = newEase;
            component.fromVector = newFromVector;
            component.toVector = newToVector;
            component.onComplete = newOnComplete;
            component.onUpdate = newOnUpdate;
            return AddComponent(ComponentIds.TweenPosition, component);
        }

        public Entity ReplaceTweenPosition(float newTime, float newDuration, int newEase, UnityEngine.Vector2 newFromVector, UnityEngine.Vector2 newToVector, System.Action<Entitas.Entity> newOnComplete, System.Action<Entitas.Entity> newOnUpdate) {
            var previousComponent = hasTweenPosition ? tweenPosition : null;
            var component = _tweenPositionComponentPool.Count > 0 ? _tweenPositionComponentPool.Pop() : new TweenPositionComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.ease = newEase;
            component.fromVector = newFromVector;
            component.toVector = newToVector;
            component.onComplete = newOnComplete;
            component.onUpdate = newOnUpdate;
            ReplaceComponent(ComponentIds.TweenPosition, component);
            if (previousComponent != null) {
                _tweenPositionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTweenPosition() {
            var component = tweenPosition;
            RemoveComponent(ComponentIds.TweenPosition);
            _tweenPositionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTweenPosition;

        public static IMatcher TweenPosition {
            get {
                if (_matcherTweenPosition == null) {
                    _matcherTweenPosition = Matcher.AllOf(ComponentIds.TweenPosition);
                }

                return _matcherTweenPosition;
            }
        }
    }
}
