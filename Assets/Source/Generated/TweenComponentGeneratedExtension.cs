using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public TweenComponent tween { get { return (TweenComponent)GetComponent(ComponentIds.Tween); } }

        public bool hasTween { get { return HasComponent(ComponentIds.Tween); } }

        static readonly Stack<TweenComponent> _tweenComponentPool = new Stack<TweenComponent>();

        public static void ClearTweenComponentPool() {
            _tweenComponentPool.Clear();
        }

        public Entity AddTween(float newTime, float newDuration, int newEase, float newFrom, float newTo, float newCurrent, System.Action<float> newOnUpdate, System.Action newOnComplete) {
            var component = _tweenComponentPool.Count > 0 ? _tweenComponentPool.Pop() : new TweenComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.ease = newEase;
            component.from = newFrom;
            component.to = newTo;
            component.current = newCurrent;
            component.onUpdate = newOnUpdate;
            component.onComplete = newOnComplete;
            return AddComponent(ComponentIds.Tween, component);
        }

        public Entity ReplaceTween(float newTime, float newDuration, int newEase, float newFrom, float newTo, float newCurrent, System.Action<float> newOnUpdate, System.Action newOnComplete) {
            var previousComponent = hasTween ? tween : null;
            var component = _tweenComponentPool.Count > 0 ? _tweenComponentPool.Pop() : new TweenComponent();
            component.time = newTime;
            component.duration = newDuration;
            component.ease = newEase;
            component.from = newFrom;
            component.to = newTo;
            component.current = newCurrent;
            component.onUpdate = newOnUpdate;
            component.onComplete = newOnComplete;
            ReplaceComponent(ComponentIds.Tween, component);
            if (previousComponent != null) {
                _tweenComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTween() {
            var component = tween;
            RemoveComponent(ComponentIds.Tween);
            _tweenComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTween;

        public static IMatcher Tween {
            get {
                if (_matcherTween == null) {
                    _matcherTween = Matcher.AllOf(ComponentIds.Tween);
                }

                return _matcherTween;
            }
        }
    }
}
