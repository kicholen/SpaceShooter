using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MagnetComponent magnet { get { return (MagnetComponent)GetComponent(ComponentIds.Magnet); } }

        public bool hasMagnet { get { return HasComponent(ComponentIds.Magnet); } }

        static readonly Stack<MagnetComponent> _magnetComponentPool = new Stack<MagnetComponent>();

        public static void ClearMagnetComponentPool() {
            _magnetComponentPool.Clear();
        }

        public Entity AddMagnet(float newRadius) {
            var component = _magnetComponentPool.Count > 0 ? _magnetComponentPool.Pop() : new MagnetComponent();
            component.radius = newRadius;
            return AddComponent(ComponentIds.Magnet, component);
        }

        public Entity ReplaceMagnet(float newRadius) {
            var previousComponent = hasMagnet ? magnet : null;
            var component = _magnetComponentPool.Count > 0 ? _magnetComponentPool.Pop() : new MagnetComponent();
            component.radius = newRadius;
            ReplaceComponent(ComponentIds.Magnet, component);
            if (previousComponent != null) {
                _magnetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMagnet() {
            var component = magnet;
            RemoveComponent(ComponentIds.Magnet);
            _magnetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMagnet;

        public static IMatcher Magnet {
            get {
                if (_matcherMagnet == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Magnet);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMagnet = matcher;
                }

                return _matcherMagnet;
            }
        }
    }
}
