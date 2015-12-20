using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public TweenComponent tween { get { return (TweenComponent)GetComponent(ComponentIds.Tween); } }

        public bool hasTween { get { return HasComponent(ComponentIds.Tween); } }

        static readonly Stack<TweenComponent> _tweenComponentPool = new Stack<TweenComponent>();

        public static void ClearTweenComponentPool() {
            _tweenComponentPool.Clear();
        }

        public Entity AddTween(bool newIsInGame, System.Collections.Generic.Dictionary<System.Type, Tween> newTweens) {
            var component = _tweenComponentPool.Count > 0 ? _tweenComponentPool.Pop() : new TweenComponent();
            component.isInGame = newIsInGame;
            component.tweens = newTweens;
            return AddComponent(ComponentIds.Tween, component);
        }

        public Entity ReplaceTween(bool newIsInGame, System.Collections.Generic.Dictionary<System.Type, Tween> newTweens) {
            var previousComponent = hasTween ? tween : null;
            var component = _tweenComponentPool.Count > 0 ? _tweenComponentPool.Pop() : new TweenComponent();
            component.isInGame = newIsInGame;
            component.tweens = newTweens;
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
