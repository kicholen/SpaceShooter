using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public BackgroundComponent background { get { return (BackgroundComponent)GetComponent(ComponentIds.Background); } }

        public bool hasBackground { get { return HasComponent(ComponentIds.Background); } }

        static readonly Stack<BackgroundComponent> _backgroundComponentPool = new Stack<BackgroundComponent>();

        public static void ClearBackgroundComponentPool() {
            _backgroundComponentPool.Clear();
        }

        public Entity AddBackground(UnityEngine.Color newColor, int newStarsCount, UnityEngine.Vector2 newDimension) {
            var component = _backgroundComponentPool.Count > 0 ? _backgroundComponentPool.Pop() : new BackgroundComponent();
            component.color = newColor;
            component.starsCount = newStarsCount;
            component.dimension = newDimension;
            return AddComponent(ComponentIds.Background, component);
        }

        public Entity ReplaceBackground(UnityEngine.Color newColor, int newStarsCount, UnityEngine.Vector2 newDimension) {
            var previousComponent = hasBackground ? background : null;
            var component = _backgroundComponentPool.Count > 0 ? _backgroundComponentPool.Pop() : new BackgroundComponent();
            component.color = newColor;
            component.starsCount = newStarsCount;
            component.dimension = newDimension;
            ReplaceComponent(ComponentIds.Background, component);
            if (previousComponent != null) {
                _backgroundComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveBackground() {
            var component = background;
            RemoveComponent(ComponentIds.Background);
            _backgroundComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBackground;

        public static IMatcher Background {
            get {
                if (_matcherBackground == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Background);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBackground = matcher;
                }

                return _matcherBackground;
            }
        }
    }
}
